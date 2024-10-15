using StackExchange.Redis;
using SuperStore.Core.Entities;
using SuperStore.Core.Repositery.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SuperStore.Repositery
{
    public class BasketRepositery : IBasketRepositery
    {
        private readonly IDatabase _database;
        public BasketRepositery(IConnectionMultiplexer Redis)
        {
            _database=Redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
           return await _database.KeyDeleteAsync(BasketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string BasketId)
        {
            var Basket =await _database.StringGetAsync(BasketId);
            if(Basket.IsNull)
                return null;
            
            return JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket)
        {
            var CreatedOrUpdated = await _database.StringSetAsync(Basket.Id,JsonSerializer.Serialize(Basket),TimeSpan.FromDays(90));
            if(CreatedOrUpdated.Equals(null)) return null;
            return await GetBasketAsync(Basket.Id);
        }
    }
}
