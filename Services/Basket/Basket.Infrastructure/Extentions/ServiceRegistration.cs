using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Infrastructure.Extentions;

public static class ServiceRegistration
{
    public static IServiceCollection AddServicesFromBasketInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //Redis Settings
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetSection("CacheSettings:ConnectionString").Value;
        });

        return services;
    }
}