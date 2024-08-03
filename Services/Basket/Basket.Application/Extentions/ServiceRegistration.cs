using Basket.Application.Behaviors;
using Basket.Application.Exceptions;
using Discount.Grpc.Protos;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Basket.Application.Extentions;

public static class ServiceRegistration
{
    public static IServiceCollection AddServicesFromBasketApplication(this IServiceCollection services, IConfiguration config)
    {
        // registers all the validator(validation) instances that are found in the assembly containing
        //services.AddValidatorsFromAssemblyContaining<CreateProductCommandValidator>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // c1
        services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o => o.Address = new Uri(config.GetSection("GrpcSettings:DiscountUrl").Value.ToString()));

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

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