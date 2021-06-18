using Course.Shared.Dtos;
using Course.Web.Models.Discounts;
using Course.Web.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Course.Web.Services.Concrede
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient _httpClient;

        public DiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //Request from  /[controller]/[action]/{code}
        public async Task<DiscountViewModel> GetDiscount(string discountCode)
        {
            var response = await _httpClient.GetAsync($"discount/GetByCode/{discountCode}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var discountResponse = await response.Content.ReadFromJsonAsync<Response<DiscountViewModel>>();
            return discountResponse.Data;
        }
    }
}
