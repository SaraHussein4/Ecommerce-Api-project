using ECommerce.Core.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Repository.Data
{
   public static class StoreContextSeed
    {
       

        public static async Task SeedAsync(StoreContext dbContext)
        {
            if (!dbContext.ProductBrands.Any())
            {
                var BrandsData = File.ReadAllText("../ECommerce.Repository/Data/DataSeed/ProductBrand.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                if (Brands?.Count > 0)
                {
                    foreach (var Brand in Brands)
                    {
                        await dbContext.Set<ProductBrand>().AddAsync(Brand);


                    }
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.ProductTypes.Any())
            {

                var TypesData = File.ReadAllText("../ECommerce.Repository/Data/DataSeed/ProductType.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                if (Types?.Count > 0)
                {
                    foreach (var Type in Types)
                    {
                        await dbContext.Set<ProductType>().AddAsync(Type);
                    }
                    await dbContext.SaveChangesAsync();

                }
            }

            if (!dbContext.Products.Any())
            {
                var ProductData = File.ReadAllText("../ECommerce.Repository/Data/DataSeed/Product.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                if (Products?.Count > 0)
                {
                    foreach (var product in Products)
                    {
                        Console.WriteLine($"Seeding Product: {product.name}, BrandId: {product.ProductBrandId}, TypeId: {product.ProductTypeId}");

                        await dbContext.Set<Product>().AddAsync(product);
                    }
                    await dbContext.SaveChangesAsync();

                }
            }

        }
    }
}
