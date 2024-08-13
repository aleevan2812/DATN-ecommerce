using Inventory.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Category>().HasData(
    new Category { Id = Guid.Parse("c0a802bf-4bfc-45f0-82f7-31836d5db0d7"), Name = "Action", DisplayOrder = 1 },
    new Category { Id = Guid.Parse("f2e8f7e4-dfcc-4ff5-a232-f2d8c8b6d6bb"), Name = "SciFi", DisplayOrder = 2 },
    new Category { Id = Guid.Parse("b70e2f35-9b9b-4267-8a49-fd4670af9074"), Name = "History", DisplayOrder = 3 }
);

        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = Guid.Parse("23a8bde2-cd1f-4721-bb0f-18e27fd9a945"),
                Title = "Fortune of Time",
                Author = "Billy Spark",
                Seller = "alee",
                Description =
                    "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                ISBN = "SWD9999001",
                Price = 90,
                StockQuantity = 10,
                SoldQuantity = 1,
                CategoryId = Guid.Parse("c0a802bf-4bfc-45f0-82f7-31836d5db0d7")
            },
            new Product
            {
                Id = Guid.Parse("ca9b37f1-5b3a-489f-8f79-8aebfa13c80f"),
                Title = "Dark Skies",
                Author = "Nancy Hoover",
                Seller = "alee",
                Description =
                    "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                ISBN = "CAW777777701",
                StockQuantity = 10,
                SoldQuantity = 1,
                Price = 30,

                CategoryId = Guid.Parse("c0a802bf-4bfc-45f0-82f7-31836d5db0d7")
            },
            new Product
            {
                Id = Guid.Parse("8cfab7e6-78a0-4a4f-9474-5cf7d724ee69"),
                Title = "Vanish in the Sunset",
                Author = "Julian Button",
                Seller = "alee",
                Description =
                    "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                ISBN = "RITO5555501",
                Price = 50,

                StockQuantity = 10,
                SoldQuantity = 1,
                CategoryId = Guid.Parse("c0a802bf-4bfc-45f0-82f7-31836d5db0d7")
            },
            new Product
            {
                Id = Guid.Parse("2d8bf6a5-d582-4a70-89f1-abc914cc48b9"),
                Title = "Cotton Candy",
                Author = "Abby Muscles",
                Seller = "rahul",
                Description =
                    "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                ISBN = "WS3333333301",
                Price = 65,

                StockQuantity = 10,
                SoldQuantity = 1,
                CategoryId = Guid.Parse("f2e8f7e4-dfcc-4ff5-a232-f2d8c8b6d6bb")
            },
            new Product
            {
                Id = Guid.Parse("c888a209-d6fb-4b36-a9d1-bcf91508d5b1"),
                Title = "Rock in the Ocean",
                Author = "Ron Parker",
                Seller = "rahul",

                Description =
                    "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                ISBN = "SOTJ1111111101",
                Price = 27,

                StockQuantity = 10,
                SoldQuantity = 1,
                CategoryId = Guid.Parse("f2e8f7e4-dfcc-4ff5-a232-f2d8c8b6d6bb")
            },
            new Product
            {
                Id = Guid.Parse("e0b095e5-278d-4425-8e62-c108a1dbde30"),
                Title = "Leaves and Wonders",
                Author = "Laura Phantom",
                Seller = "rahul",

                Description =
                    "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                ISBN = "FOT000000001",
                Price = 23,

                StockQuantity = 10,
                SoldQuantity = 1,
                CategoryId = Guid.Parse("b70e2f35-9b9b-4267-8a49-fd4670af9074")
            }
        );
    }
}