using Catalog.Application.Behaviors;
using Catalog.Application.Exceptions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Catalog.Application.Extentions;

public static class ServiceRegistration
{
    public static IServiceCollection AddServicesFromCatalogApplication(this IServiceCollection services)
    {
        // registers all the validator(validation) instances that are found in the assembly containing
        // other way is below at AddFluentValidationFromCatalogApplication
        //services.AddValidatorsFromAssemblyContaining<CreateProductCommandValidator>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {
            // register Handlers from MediatR
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            cfg.AddOpenBehavior(typeof(RequestResponseLoggingBehavior<,>));

            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestResponseLoggingBehavior<,>));
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        // The above ensures that your IExceptionHandler implementation is registered into the service container of the application
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }

    public static IMvcBuilder AddFluentValidationFromCatalogApplication(this IMvcBuilder mvcBuilder)
    {
        // registers all the validator instances that are found in the assembly containing
        // other way is above at AddServicesFromCatalogApplication
        //mvcBuilder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateProductCommandValidator>());

        return mvcBuilder;
    }
}