using Discount.Grpc.Protos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Basket.Application.Extentions;

public static class ServiceRegistration
{
    public static IServiceCollection AddServicesFromBasketApplication(this IServiceCollection services, IConfiguration config)
    {
        services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>
            (o => o.Address = new Uri(config.GetSection("GrpcSettings:DiscountUrl").Value));

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {
            // register Handlers from MediatR
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}