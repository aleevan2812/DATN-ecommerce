using Catalog.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public class CatalogContext : ICatalogContext
{
    public IMongoCollection<Product> Products { get; }
    public IMongoCollection<ProductBrand> Brands { get; }
    public IMongoCollection<ProductType> Types { get; }

    public CatalogContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetSection("DatabaseSettings:ConnectionString").Value);
        var database = client.GetDatabase(configuration.GetSection("DatabaseSettings:DatabaseName").Value);
        Brands = database.GetCollection<ProductBrand>(
            configuration.GetSection("DatabaseSettings:BrandsCollection").Value);
        Types = database.GetCollection<ProductType>(
            configuration.GetSection("DatabaseSettings:TypesCollection").Value);
        Products = database.GetCollection<Product>(
            configuration.GetSection("DatabaseSettings:CollectionName").Value);

        BrandContextSeed.SeedData(Brands);
        TypeContextSeed.SeedData(Types);
        CatalogContextSeed.SeedData(Products);
    }
}