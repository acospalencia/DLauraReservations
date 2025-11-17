using DLaura.DataAcces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DLaura.BusinessLogic
{
    public static class BusinessLogicExtensions
    {
        public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddDataAccesServices(configuration);
            return services;
        }
    }
}
