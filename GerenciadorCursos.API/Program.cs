using GerenciadorCursos.Application.Handlers;
using GerenciadorCursos.Domain.Interfaces;
using GerenciadorCursos.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

// 1? Configurar conexão mySql (Infrastructure)
var mysqlconnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<GerenciadorCursosContext>(options =>
    options.UseMySql(mysqlconnection, ServerVersion.AutoDetect(mysqlconnection),
     b => b.MigrationsAssembly("GerenciadorCursos.Infrastructure") // <- migrations no projeto Infrastructure
    ));

// 2? Registrar UnitOfWork (Infrastructure) e Handlers (Application)
builder.Services.AddDepencies();

// 3? Registrar Controllers
builder.Services.AddControllers();

// 4? Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 5? Configurar pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
