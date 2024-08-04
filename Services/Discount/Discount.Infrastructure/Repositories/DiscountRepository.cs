using Dapper;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.Infrastructure.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly IConfiguration _configuration;

    public DiscountRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Coupon> GetDiscount(string productId)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetSection("DatabaseSettings:ConnectionString").Value);
        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
            ("SELECT * FROM Coupon WHERE ProductId = @ProductId", new { ProductId = productId });
        if (coupon == null)
            return new Coupon { ProductId = "-1", Amount = 0, Description = "No Discount Available" };
        return coupon;
    }

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetSection("DatabaseSettings:ConnectionString").Value);

        var affected =
            await connection.ExecuteAsync
            ("INSERT INTO Coupon (ProductId, Description, Amount) VALUES (@ProductId, @Description, @Amount)",
                new { ProductId = coupon.ProductId, Description = coupon.Description, Amount = coupon.Amount, Quantity = coupon.Quantity });

        if (affected == 0)
            return false;

        return true;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetSection("DatabaseSettings:ConnectionString").Value);

        var affected = await connection.ExecuteAsync
        ("UPDATE Coupon SET ProductId=@ProductId, Description = @Description, Amount = @Amount WHERE Id = @Id",
            new { ProductId = coupon.ProductId, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id, Quantity = coupon.Quantity });

        if (affected == 0)
            return false;

        return true;
    }

    public async Task<bool> DeleteDiscount(string productId)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetSection("DatabaseSettings:ConnectionString").Value);

        var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductId = @ProductId",
            new { ProductId = productId });

        if (affected == 0)
            return false;

        return true;
    }
}