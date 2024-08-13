using Alee_BookEcommerceAPI.Repository;
using Inventory.Core.IRepository;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infrastructure.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddServicesFromInventoryInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseNpgsql(config.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        //services.AddScoped<IImageService, ImageSevice>();

        return services;
    }
}