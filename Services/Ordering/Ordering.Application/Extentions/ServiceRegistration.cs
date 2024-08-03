using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ordering.Application.Extentions;

public static class ServiceRegistration
{
    public static IServiceCollection AddDiscountApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {
            // register Handlers from MediatR
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}