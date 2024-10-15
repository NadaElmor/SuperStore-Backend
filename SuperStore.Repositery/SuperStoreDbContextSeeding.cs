using Store.Core.Entities;
using SuperStore.Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SuperStore.Repositery
{
    public class SuperStoreDbContextSeeding
    {
        public static async Task DataSeed(SuperStoreDbContext superStoreDbContext)
        {
            //product brand
            if (!superStoreDbContext.ProductBrands.Any())
            {
                var BrandsData = File.ReadAllText("../SuperStore.Repositery/Data/DataSeeding/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                if (Brands?.Count > 0)
                {
                    foreach (var Brand in Brands)
                    {
                        await superStoreDbContext.Set<ProductBrand>().AddAsync(Brand);
                    }
                    await superStoreDbContext.SaveChangesAsync();
                }
            }
            //types
            if (!superStoreDbContext.ProductTypes.Any())
            {
                var TypesData = File.ReadAllText("../SuperStore.Repositery/Data/DataSeeding/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                if (types?.Count > 0)
                {
                    foreach (var type in types)
                    {
                        await superStoreDbContext.Set<ProductType>().AddAsync(type);
                    }
                    await superStoreDbContext.SaveChangesAsync();
                }
            }
            //products
            if (!superStoreDbContext.Products.Any())
            {
                var ProductsData = File.ReadAllText("../SuperStore.Repositery/Data/DataSeeding/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                if (products?.Count > 0)
                {
                    foreach (var product in products)
                    {
                        await superStoreDbContext.Set<Product>().AddAsync(product);
                    }
                    await superStoreDbContext.SaveChangesAsync();
                }
            }
            //Delivery Methods
            //products
            if (!superStoreDbContext.DeliveryMethods.Any())
            {
                var DeliveryMethodsData = File.ReadAllText("../SuperStore.Repositery/Data/DataSeeding/delivery.json");
                var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsData);
                if (DeliveryMethods?.Count > 0)
                {
                    foreach (var DeliveryMethod in DeliveryMethods)
                    {
                        await superStoreDbContext.Set<DeliveryMethod>().AddAsync(DeliveryMethod);
                    }
                    await superStoreDbContext.SaveChangesAsync();
                }
            }

        }
    }
}
