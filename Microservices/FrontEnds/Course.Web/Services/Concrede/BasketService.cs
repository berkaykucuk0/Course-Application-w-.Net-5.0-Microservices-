using Course.Web.Models.Baskets;
using Course.Web.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Web.Services.Concrede
{
    public class BasketService : IBasketService
    {
        public async Task AddBasketItemAsync(BasketItemViewModel basketItemViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ApplyDiscountAsync(string discountCode)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CancelApplyDiscountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BasketViewModel> GetAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveBasketItemAsync(string courseId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveOrUpdateAsync(BasketViewModel basketViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
