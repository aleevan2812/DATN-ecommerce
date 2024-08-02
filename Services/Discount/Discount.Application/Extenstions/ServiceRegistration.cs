using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Discount.Application.Extentions;

public static class ServiceRegistration
{
    public static IServiceCollection AddServicesFromDiscountApplication(this IServiceCollection services)
    {
        services.AddGrpc();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {
            // register Handlers from MediatR
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}