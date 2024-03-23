using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Services.CasheServices
{
    public class CasheService : ICasheServices
    {
        private readonly IDatabase _database;
        public CasheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<string> GetCacheResponseAsync(string cashKey)
        {
            var cachedresponse = await _database.StringGetAsync(cashKey);
            if (cachedresponse.IsNullOrEmpty)
                return null;
            return cachedresponse.ToString();
        }

        public async Task setCaheResponseAsync(string cashKey, object response, TimeSpan TimeToLive)
        {
            if (response == null) 
                return;
            var options = new JsonSerializerOptions {PropertyNamingPolicy= JsonNamingPolicy.CamelCase};
            var serializedresponse = JsonSerializer.Serialize(response,options);
            await _database.StringSetAsync(cashKey, serializedresponse, TimeToLive);

        }
    }
}
