using Discount.Grpc.Protos;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Behaviors;
using Ordering.Application.Exceptions;
using Ordering.Application.GrpcService;
using Ordering.Application.Validators;
using System.Reflection;

namespace Ordering.Application.Extentions;

public static class ServiceRegistration
{
    public static IServiceCollection AddOrderingApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddValidatorsFromAssemblyContaining<CheckoutOrderCommandValidator>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // c1
        services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o => o.Address = new Uri(config.GetSection("GrpcSettings:DiscountUrl").Value.ToString()));

        // DI
        services.AddScoped<DiscountGrpcService>();

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

        Stripe.StripeConfiguration.ApiKey = config.GetSection("Stripe:SecretKey").Get<string>();

        return services;
    }
}