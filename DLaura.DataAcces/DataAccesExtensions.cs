using DLaura.DataAcces.Interfaces;
using DLaura.DataAcces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace DLaura.DataAcces
{
    public static class DataAccesExtensions
    {
        public static IServiceCollection AddDataAccesServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DonaLauraContext>(Options =>
            {
                Options.UseSqlServer(configuration.GetConnectionString("DbConnection") ??
                    throw new InvalidOperationException("Connection string not found"));
            });
            services.AddTransient(typeof(IEfRepository<>), typeof(EfRepository<>));

            return services;
        }
    }
}
