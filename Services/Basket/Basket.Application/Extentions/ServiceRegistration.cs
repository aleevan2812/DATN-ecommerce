using Discount.Grpc.Protos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Application.Extentions;

public static class ServiceRegistration
{
    public static IServiceCollection AddServicesFromBasketApplication(this IServiceCollection services, IConfiguration config)
    {
        services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>
            (o => o.Address = new Uri(config.GetSection("GrpcSettings:DiscountUrl").Value));

        return services;
    }
}