using Discount.Core.Repositories;
using Discount.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Discount.Infrastructure.Extentions;

public static class ServiceRegistration
{
    public static IServiceCollection MigrateDatabase(this IServiceCollection services, IConfiguration config)
    {
        try
        {
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            ApplyMigrations(config);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return services;
    }

    private static void ApplyMigrations(IConfiguration config)
    {
        using var connection = new NpgsqlConnection(config.GetSection("DatabaseSettings:ConnectionString").Value);
        connection.Open();
        using var cmd = new NpgsqlCommand()
        {
            Connection = connection
        };
        cmd.CommandText = "DROP TABLE IF EXISTS Coupon";
        cmd.ExecuteNonQuery();
        cmd.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY,
                                                ProductId VARCHAR(500) NOT NULL,
                                                Description TEXT,
                                                Amount INT,
                                                Quantity INT)";
        cmd.ExecuteNonQuery();

        cmd.CommandText = "INSERT INTO Coupon(ProductId, Description, Amount, Quantity) VALUES('e51f9686-1c41-48cb-89ca-d96d78823f74', 'Shoe Discount : e51f9686-1c41-48cb-89ca-d96d78823f74', 500, 3);";
        cmd.ExecuteNonQuery();

        cmd.CommandText = "INSERT INTO Coupon(ProductId, Description, Amount, Quantity) VALUES('7a6d77c5-75d8-473b-8d1d-664515cc7ceb', 'Racquet Discount : 7a6d77c5-75d8-473b-8d1d-664515cc7ceb', 700, 1);";
        cmd.ExecuteNonQuery();
    }
}