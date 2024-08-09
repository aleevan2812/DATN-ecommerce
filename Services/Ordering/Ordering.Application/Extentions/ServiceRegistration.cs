using Discount.Grpc.Protos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.GrpcService;
using System.Reflection;

namespace Ordering.Application.Extentions;

public static class ServiceRegistration
{
    public static IServiceCollection AddOrderingApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        // c1
        services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o => o.Address = new Uri(config.GetSection("GrpcSettings:DiscountUrl").Value.ToString()));

        // DI
        services.AddScoped<DiscountGrpcService>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {
            // register Handlers from MediatR
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        Stripe.StripeConfiguration.ApiKey = config.GetSection("Stripe:SecretKey").Get<string>();

        return services;
    }
}