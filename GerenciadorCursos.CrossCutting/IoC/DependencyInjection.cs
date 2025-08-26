using GerenciadorCursos.Application.Handlers;
using GerenciadorCursos.Domain.Interfaces;
using GerenciadorCursos.Infrastructure.Repositories;
using GerenciadorCursos.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GerenciadorCursos.Infrastructure.Context
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDepencies(this IServiceCollection services)
        {
            // Aqui você pode adicionar outras injeções de dependência relacionadas à infraestrutura, se necessário

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<CriarAlunoHandler>();
            services.AddScoped<CriarCursoHandler>();
            services.AddScoped<CadastrarAlunoEmCursoHandler>();

            services.AddScoped<IAlunoRepository, AlunoRepository>();
            services.AddScoped<ICursoRepository, CursoRepository>();
            services.AddScoped<IInscricaoRepository, InscricaoRepository>();

            return services;
        }
    }
}
