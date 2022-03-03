using API.Services;
using Domain.Entities;
using Domain.Handlers;
using Domain.Repository;
using Infra.Contexts;
using Infra.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IO.Compression;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

SetServices();
SetAuthAndCompression();

var app = builder.Build();
SetConfig();

app.Run();

void SetConfig()
{
    if (builder.Environment.IsDevelopment())
    {
        //aqui eu falo que caso esteja rodando em desenvolvimento/debug, 
        //exeptions do HTTP vão ser lançadas como páginas HTTP
        app.UseDeveloperExceptionPage();
        app.UseExceptionHandler("/error-development");
    }
    else
        app.UseExceptionHandler("/error");


    //aqui eu forço a api a responder e ler somente em HTTPS, convertendo HTTP requests em HTTPS, caso nescesário
    app.UseHttpsRedirection();
    app.UseHsts();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseResponseCompression();

    app.UseEndpoints(endpts => endpts.MapControllers());
}

void SetServices()
{
    //------Note: The comments in this file are in my native language, so I can more easily understand what is going on

    builder.Services.AddControllers().AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    }).AddControllersAsServices();

    //Aqui eu falo qual tipo de Db o EF deve usar
    //builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("InternalDatabase"));
    builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(builder.Configuration["ConnectionStrings:Sqlserver"]));
    

    //Aqui eu falo onde o repositório e os Handlers estão, para uso dos controllers
    builder.Services.AddTransient<IRepository<Apart>, ApartRepository>();
    builder.Services.AddTransient<ApartHandler, ApartHandler>();

    builder.Services.AddTransient<IRepository<Resident>, ResidentRepository>();
    builder.Services.AddTransient<ResidentHandler, ResidentHandler>();

    builder.Services.AddTransient<IRepository<Visitant>, VisitantRepository>();
    builder.Services.AddTransient<VisitantHandler, VisitantHandler>();

    builder.Services.AddTransient<IRepository<Packages>, PackageRepository>();
    builder.Services.AddTransient<PackagesHandler, PackagesHandler>();

    builder.Services.AddTransient<IRepository<Administrator>, AdminRepository>();
    builder.Services.AddTransient<AdministratorHandler, AdministratorHandler>();

    builder.Services.AddTransient<ServiceEmail>();
}

void SetAuthAndCompression()
{
    //aqui eu add a compressão da resposta, falando o tipo de zip que vou usar e configurar o nivel de compressão...
    builder.Services.AddResponseCompression(opt =>
    {
        opt.Providers.Add<GzipCompressionProvider>();
    });


    builder.Services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
    builder.Services.AddMvc(x =>
    {
        var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
        x.Filters.Add(new AuthorizeFilter(policy));
    });

    //Aqui eu adiciono a auth. Link: https://balta.io/artigos/aspnetcore-3-autenticacao-autorizacao-bearer-jwt
        ServiceToken.Secret = builder.Configuration["Keys:TokenGenerateKey"];

    var key = Encoding.ASCII.GetBytes(ServiceToken.Secret);
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateAudience = false,
            ValidateIssuer = false
        };
    });

    var roles = builder.Configuration["JwtOptions:RoleClaim"];
    if (string.IsNullOrWhiteSpace(roles)) roles = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
    builder.Services.AddAuthorization(x =>
    {
        x.AddPolicy("Admin", p => p.RequireClaim(roles, "Admin"));
        x.AddPolicy("Resident", p => p.RequireClaim(roles, "Resident"));
    });
}