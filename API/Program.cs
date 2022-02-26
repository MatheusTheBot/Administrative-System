using Domain.Entities;
using Domain.Handlers;
using Domain.Repository;
using Infra.Contexts;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

SetServices();

var app = builder.Build();
SetConfig();

app.Run();

void SetConfig()
{
    if (builder.Environment.IsDevelopment())
    {
        //aqui eu falo que caso esteja rodando em desenvolvimento/debug, 
        //exeptions do HTTP vão ser lançadas como HTTP caso dê erros
        app.UseDeveloperExceptionPage();
    }
    //aqui eu forço a api a responder e ler somente em HTTPS, convertendo HTTP requests em HTTPS, caso nescesário
    app.UseHttpsRedirection();
    app.UseHsts();

    app.UseRouting();

    app.UseEndpoints(endpts => endpts.MapControllers());
}

void SetServices()
{
    builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles).AddControllersAsServices();

    //Aqui eu falo qual tipo de Db o EF deve usar
    builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("InternalDatabase"));
    //builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

    //Aqui eu falo onde o repositório e os Handlers estão, para uso dos controllers
    builder.Services.AddTransient<IRepository<Apart>, ApartRepository>();
    builder.Services.AddTransient<ApartHandler, ApartHandler>();

    builder.Services.AddTransient<IRepository<Resident>, ResidentRepository>();
    builder.Services.AddTransient<ResidentHandler, ResidentHandler>();

    builder.Services.AddTransient<IRepository<Visitant>, VisitantRepository>();
    builder.Services.AddTransient<VisitantHandler, VisitantHandler>();

    builder.Services.AddTransient<IRepository<Packages>, PackageRepository>();
    builder.Services.AddTransient<PackagesHandler, PackagesHandler>();
}