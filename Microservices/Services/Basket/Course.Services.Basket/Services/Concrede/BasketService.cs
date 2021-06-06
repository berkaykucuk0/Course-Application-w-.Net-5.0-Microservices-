using Course.Services.Basket.Dtos;
using Course.Services.Basket.Services.Abstract;
using Course.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Course.Services.Basket.Services.Concrede
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Response<bool>> Delete(string userId)
        {
            var status = await _redisService.GetDb().KeyDeleteAsync(userId);
            if (status)
                return Response<bool>.Success(200);
            else
                return Response<bool>.Fail("Basket couldn't delete", 500);

        }

        public async Task<Response<BasketDto>> GetBasket(string userId)
        {
            var existUser = await _redisService.GetDb().StringGetAsync(userId);
            if (existUser.IsNullOrEmpty)
            {
                return Response<BasketDto>.Fail("Basket not found", 404);
            }

            var data = JsonSerializer.Deserialize<BasketDto>(existUser);

            return Response<BasketDto>.Success(data, 200);
        }

        public async Task<Response<bool>> SaveOrUpdate(BasketDto basketDto)
        {
            var basketData = JsonSerializer.Serialize(basketDto);
            var status = await _redisService.GetDb().StringSetAsync(basketDto.UserId, basketData);

            if (status)           
                return Response<bool>.Success(200);          
            else            
                return Response<bool>.Fail("Basket couldn't update or save", 500);
            

        }
    }
}
