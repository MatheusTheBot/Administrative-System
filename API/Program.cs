using Domain.Handlers;
using Infra.Contexts;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;

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
        //exeptions do HTTP v�o ser lan�adas como HTTP caso d� erros
        app.UseDeveloperExceptionPage();
    }
    //aqui eu for�o a api a responder e ler somente em HTTPS, convertendo HTTP requests em HTTPS, caso nesces�rio
    app.UseHttpsRedirection();
    app.UseHsts();

    app.UseRouting();

    app.MapControllers();

    app.UseEndpoints(endpts => endpts.MapControllers());
}

void SetServices()
{
    builder.Services.AddControllers();

    //Aqui eu falo qual tipo de Db o EF deve usar
    builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("InternalDatabase"));
    //builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

    //Aqui eu falo onde o reposit�rio e os Handlers est�o, para uso dos controllers
    builder.Services.AddTransient<ResidentRepository, ResidentRepository>();
    builder.Services.AddTransient<ResidentHandler, ResidentHandler>();
}