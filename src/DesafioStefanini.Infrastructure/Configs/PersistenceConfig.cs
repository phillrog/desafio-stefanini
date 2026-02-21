using DesafioStefanini.Domain.Interfaces.Repositories;
using DesafioStefanini.Infrastructure.Data.Contexts;
using DesafioStefanini.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioStefanini.Infrastructure.Configs
{
    public static class PersistenceConfig
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            return services;
        }
    }
}
