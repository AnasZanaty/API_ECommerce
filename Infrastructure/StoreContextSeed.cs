using Core;
using Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class StoreContextSeed
    {
        public static async Task SeedASync (StoreDbContext context , ILoggerFactory loggerFactory)
        {
            try
            {
                if (context.productBrands!= null && !context.productBrands.Any()) {

                    var brandsData = File.ReadAllText("../Infrastructure/SeedData/brands.json");

                    //Serilization & desirialization

                    //serilization is to convert class object to a string 
                    //deserialization is to convert json to c# object

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    if (brands is not null)
                    {
                        foreach (var brand in brands)
                        {
                            await context.productBrands.AddAsync(brand);

                        }

                        await context.SaveChangesAsync();

                    }

                
                }

                if (context.productTypes != null && !context.productTypes.Any())
                {

                    var TypesData = File.ReadAllText("../Infrastructure/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);

                    if (types is not null)
                    {
                        foreach (var type in types)
                        {
                            await context.productTypes.AddAsync(type);

                        }

                        await context.SaveChangesAsync();

                    }


                }
                 if (context.Products!= null && !context.Products.Any()) {

                    var productsData = File.ReadAllText("../Infrastructure/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    if (products is not null)
                    {
                        foreach (var product in products)
                        {
                            await context.Products.AddAsync(product);

                        }

                        await context.SaveChangesAsync();


                    }
                
                }

                if (context.DeliveryMethods != null && !context.DeliveryMethods.Any())
                {

                    var deleveryMethodData = File.ReadAllText("../Infrastructure/SeedData/delivery.json");

                    var Deliveries = JsonSerializer.Deserialize<List<DeliveryMethod>>(deleveryMethodData);

                    if (Deliveries is not null)
                    {
                        foreach (var delivery in Deliveries)
                        {
                            await context.DeliveryMethods.AddAsync(delivery);

                        }

                        await context.SaveChangesAsync();


                    }

                }

            }
            catch (Exception ex) {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
