using Course.Web.Models.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Web.Services.Abstract
{
    public interface IBasketService
    {
        Task<bool> SaveOrUpdateAsync(BasketViewModel basketViewModel);
        Task<BasketViewModel> GetAsync();
        Task<bool> DeleteAsync();
        Task AddBasketItemAsync(BasketItemViewModel basketItemViewModel);
        Task<bool> RemoveBasketItemAsync(string courseId);
        Task<bool> ApplyDiscountAsync(string discountCode);
        Task<bool> CancelApplyDiscountAsync();

    }
}
