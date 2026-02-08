using E_Commerce.Domian.Entites;
using E_Commerce.Domian.Entites.OrderModule;
using E_Commerce.Domian.Entites.ProductModule;
using E_Commerce.Domian.Interfaces;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.SeedData
{
    public class DataInitializer : IDataInitializer
    {
        private readonly StoreDbContext _dbContext;

        public DataInitializer(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InitializeAsync()
        {
            try
            {
                var hasProducts = await _dbContext.Products.AnyAsync();
                var hasProductBrands = await _dbContext.ProductBrands.AnyAsync();
                var hasProductTypes = await _dbContext.ProductTypes.AnyAsync();
                var hasDeliveryMethods = await _dbContext.DeliveryMethods.AnyAsync();


                if (hasProducts && hasProductBrands && hasProductTypes && hasDeliveryMethods) return;

                var basePath = Path.Combine(AppContext.BaseDirectory, "Data", "SeedData", "Files");

                if (!hasProductBrands)
                {
                    await SeedDataFromJsonAsync<ProductBrand, int>(Path.Combine(basePath, "brands.json"), _dbContext.ProductBrands);
                }
                if (!hasProductTypes)
                {
                    await SeedDataFromJsonAsync<ProductType, int>(Path.Combine(basePath, "types.json"), _dbContext.ProductTypes);
                }
                await _dbContext.SaveChangesAsync();
                if (!hasProducts)
                {
                    await SeedDataFromJsonAsync<Product, int>(Path.Combine(basePath, "products.json"), _dbContext.Products);
                }
                await _dbContext.SaveChangesAsync();
                if (!hasDeliveryMethods)
                {
                    await SeedDataFromJsonAsync<DeliveryMethod, int>(Path.Combine(basePath, "delivery.json"), _dbContext.DeliveryMethods);
                }
                await _dbContext.SaveChangesAsync();



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async Task SeedDataFromJsonAsync<TEnitiy, TKey>(string filePath, DbSet<TEnitiy> entity) where TEnitiy : BaseEntity<TKey>
        {
            if (!File.Exists(filePath)) throw new FileNotFoundException();

            try
            {

                using var reader = File.OpenRead(filePath);

                var data = await JsonSerializer.DeserializeAsync<IEnumerable<TEnitiy>>(reader, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });

                if (data is not null)
                {
                    await entity.AddRangeAsync(data);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }



        }
    }
}
