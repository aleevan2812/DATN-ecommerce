﻿using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data;

public class CatalogContextSeed
{
    public static void SeedData(IMongoCollection<Product> productCollection)
    {
        bool checkProducts = productCollection.Find(b => true).Any();

        string relativePath = Path.Combine("Data", "SeedData", "products.json");
        string fullPath = Path.GetFullPath(relativePath);
        //string path = Path.Combine("Data", "SeedData", "products.json");
        if (!checkProducts)
        {
            var productsData = File.ReadAllText(fullPath);
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            if (products != null)
            {
                foreach (var item in products)
                {
                    productCollection.InsertOneAsync(item);
                }
            }
        }
    }
}