using Infrastructure.BasketReposatory.BasketEntities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace Infrastructure.BasketReposatory
{
    public class BasketReposatory : IBasketReposatory
    {
        private readonly IDatabase _database;
        public BasketReposatory(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            var delete = await _database.KeyDeleteAsync(basketId);
            return delete;
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);

        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var isCreated = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            if (!isCreated)
                return null;

            return await GetBasketAsync(basket.Id);
        }
    }
}
