using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Catalog.Application.Extentions;

public static class ServiceRegistration
{
    public static IServiceCollection AddServicesFromCatalogApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}