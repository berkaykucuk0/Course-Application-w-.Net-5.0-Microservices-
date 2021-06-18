using Course.Shared.Dtos;
using Course.Web.Models.Baskets;
using Course.Web.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Course.Web.Services.Concrede
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;
        private readonly IDiscountService _discountService;
        public BasketService(HttpClient httpClient, IDiscountService discountService)
        {
            _httpClient = httpClient;
            _discountService = discountService;
        }

        public async Task AddBasketItemAsync(BasketItemViewModel basketItemViewModel)
        {
            var basket = await GetAsync();
            if (basket!=null)
            {
                //If that course is not in the cart, add it
                if (!basket.BasketItems.Any(x=>x.CourseId== basketItemViewModel.CourseId))
                {
                    basket.BasketItems.Add(basketItemViewModel);
                }
            }
            else //if the basket is empty
            {
                basket = new BasketViewModel();
                basket.BasketItems.Add(basketItemViewModel);
            }
           await  SaveOrUpdateAsync(basket);
        }

        public async Task<bool> ApplyDiscountAsync(string discountCode)
        {         
            await CancelApplyDiscountAsync();

            var basket = await GetAsync();
            if (basket == null)
            {
                return false;
            }

            var hasDiscount = await _discountService.GetDiscount(discountCode);
            if (hasDiscount == null)
            {
                return false;
            }
            basket.ApplyDiscount(hasDiscount.Code, hasDiscount.Rate);
            await SaveOrUpdateAsync(basket);
            return true;
        }

        public async Task<bool> CancelApplyDiscountAsync()
        {
            var basket = await GetAsync();

            if (basket == null || basket.DiscountCode == null)
            {
                return false;
            }

            basket.CancelDiscount();
            await SaveOrUpdateAsync(basket);
            return true;

        }

        public async Task<bool> DeleteAsync()
        {
            var response = await _httpClient.DeleteAsync("basket");
            return response.IsSuccessStatusCode;
        }

        public async Task<BasketViewModel> GetAsync()
        {
            var response = await _httpClient.GetAsync("basket");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var basketViewModel = await response.Content.ReadFromJsonAsync<Response<BasketViewModel>>();
            return basketViewModel.Data;
        }

        public async Task<bool> RemoveBasketItemAsync(string courseId)
        {
            var basket = await GetAsync();
            if (basket==null)
            {
                return false;
            }
            var deleteItem = basket.BasketItems.FirstOrDefault(x => x.CourseId == courseId);
            if (deleteItem==null)
            {
                return false;
            }

            var deleteResult = basket.BasketItems.Remove(deleteItem);
            if (!deleteResult)
            {
                return false;
            }

            //if we deleted last item in cart , discount should be null
            if (!basket.BasketItems.Any())
            {
                basket.DiscountCode = null;
            }

            return await SaveOrUpdateAsync(basket);
        }

        public async Task<bool> SaveOrUpdateAsync(BasketViewModel basketViewModel)
        {
            var response = await _httpClient.PostAsJsonAsync<BasketViewModel>("basket", basketViewModel);
            return response.IsSuccessStatusCode;
        }
    }
}
