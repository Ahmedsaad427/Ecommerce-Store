using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _context;

        public DbInitializer(StoreDbContext context)
        {
            _context = context;
        }

        public async Task IntializeAsync()
        {
            try
            {
                // Apply any pending migrations
                if (_context.Database.GetPendingMigrations().Any())
                {
                    await _context.Database.MigrateAsync();
                }

                // Seeding Product Types from JSON file
                if (!_context.ProductTypes.Any())
                {
                    // 1 - Read all data from types.json file as a string
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");

                    // 2 - Deserialize the JSON string into a list of ProductType objects
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    // 3 - Add the list of ProductType to the database
                    if (types is not null && types.Any())
                    {
                        await _context.ProductTypes.AddRangeAsync(types);

                        // 4 - Save the changes to the database
                        await _context.SaveChangesAsync();
                    }
                }

                // Seeding Product Brands from JSON file
                if (!_context.ProductBrands.Any())
                {
                    // 1 - Read all data from brands.json file as a string
                    var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");

                    // 2 - Deserialize the JSON string into a list of ProductBrand objects
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    // 3 - Add the list of ProductBrand to the database
                    if (brands is not null && brands.Any())
                    {
                        await _context.ProductBrands.AddRangeAsync(brands);

                        // 4 - Save the changes to the database
                        await _context.SaveChangesAsync();
                    }
                }

                // Seeding Products from JSON file
                if (!_context.Products.Any())
                {
                    // 1 - Read all data from products.json file as a string
                    var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");

                    // 2 - Deserialize the JSON string into a list of Product objects
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    // 3 - Add the list of Product to the database
                    if (products is not null && products.Any())
                    {
                        await _context.Products.AddRangeAsync(products);

                        // 4 - Save the changes to the database
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                Console.WriteLine($"Error during database initialization: {ex.Message}");
            }
        }
    }
}
