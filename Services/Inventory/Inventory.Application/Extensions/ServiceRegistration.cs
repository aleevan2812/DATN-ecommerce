using FluentValidation;
using Inventory.Application.Behaviors;
using Inventory.Application.Exceptions;
using Inventory.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Inventory.Application.Extentions;

public static class ServiceRegistration
{
    public static IServiceCollection AddServicesFromInventoryApplication(this IServiceCollection services, IConfiguration config)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<CloudinaryService>();

        services.AddMediatR(cfg =>
        {
            // register Handlers from MediatR
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            cfg.AddOpenBehavior(typeof(RequestResponseLoggingBehavior<,>));

            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        // The above ensures that your IExceptionHandler implementation is registered into the service container of the application
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }
}