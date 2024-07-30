using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data;

public static class BrandContextSeed
{
    public static void SeedData(IMongoCollection<ProductBrand> brandCollection)
    {
        try
        {
            bool checkBrands = brandCollection.Find(b => true).Any();
            string relativePath = Path.Combine("Data", "SeedData", "brands.json");
            string fullPath = Path.GetFullPath(relativePath);
            //string path = Path.Combine("Data", "SeedData", "brands.json");
            if (!checkBrands)
            {
                var brandsData = File.ReadAllText(fullPath);
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands != null)
                {
                    foreach (var item in brands)
                    {
                        brandCollection.InsertOneAsync(item);
                    }
                }
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}