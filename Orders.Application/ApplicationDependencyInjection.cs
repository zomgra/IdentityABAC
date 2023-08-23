using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Orders.Application
{
    public static class ApplicationDependencyInjection 
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(x =>
            {
                x.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });
            return services;
        }
    }
}
