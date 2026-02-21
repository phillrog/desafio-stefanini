using DesafioStefanini.Application.Interfaces.Services;
using DesafioStefanini.Application.Mappings;
using DesafioStefanini.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioStefanini.Infrastructure.Configs
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // assembly path
            var applicationAssembly = typeof(PedidoMappingProfile).Assembly;

            // Registrar o AutoMapper usando o assembly da Application
            services.AddAutoMapper(applicationAssembly);

            // Validadores
            services.AddValidatorsFromAssembly(applicationAssembly);

            // Services
            services.AddScoped<IPedidoService, PedidoService>();
            services.AddScoped<IProdutoService, ProdutoService>();

            return services;
        }
    }
}