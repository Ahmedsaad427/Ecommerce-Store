using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using StackExchange.Redis;
using Domain.Models; 

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
      
        private readonly IDatabase _database= connection.GetDatabase();

        public async Task<CustomerBasket> GetBasketAsync(string id)
        {
            var redisValue = await _database.StringGetAsync(id);
            if (redisValue.IsNullOrEmpty)
            {
                return null;
            }
            var basket = JsonSerializer.Deserialize<CustomerBasket>(redisValue!);
            if (basket == null)
            {
                return null;
            }
            return basket;

        }

        public async Task<Domain.Models.CustomerBasket> UpdateBasketAsync(Domain.Models.CustomerBasket basket, TimeSpan? timeToLive)
        {
            var redisValue = JsonSerializer.Serialize(basket);

            // Set expiration to given TTL or default to 30 days
            var expiration = timeToLive ?? TimeSpan.FromDays(30);

            // Save to Redis
            bool success = await _database.StringSetAsync(basket.Id, redisValue, expiration);

            if (!success)
            {
                return null; // or throw an exception if you'd rather not return null
            }

            return basket;
        }


        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            // Removes the key (basketId) from Redis
            return await _database.KeyDeleteAsync(basketId);
        }

    }
}
