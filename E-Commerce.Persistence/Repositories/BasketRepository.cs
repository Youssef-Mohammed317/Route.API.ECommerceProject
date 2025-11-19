using E_Commerce.Domian.Entites.BasketModule;
using E_Commerce.Domian.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan timeToLive = default)
        {
            if (basket.Id == Guid.Empty)
            {
                basket.Id = Guid.NewGuid();
            }
            var IsCreatedOrUpdated = await _database.StringSetAsync(basket.Id.ToString(), JsonSerializer.Serialize(basket), timeToLive == default ? TimeSpan.FromDays(7) : timeToLive);

            if (IsCreatedOrUpdated)
            {
                return await GetBasketAsync(basket.Id);
            }
            return null;
        }

        public Task<bool> DeleteBasketAsync(Guid basketId)
        {
            return _database.KeyDeleteAsync(basketId.ToString());
        }

        public async Task<CustomerBasket?> GetBasketAsync(Guid basketId)
        {
            var basket = await _database.StringGetAsync(basketId.ToString());
            if (!basket.IsNullOrEmpty)
            {
                return JsonSerializer.Deserialize<CustomerBasket>(basket!);
            }
            return null;
        }
    }
}
