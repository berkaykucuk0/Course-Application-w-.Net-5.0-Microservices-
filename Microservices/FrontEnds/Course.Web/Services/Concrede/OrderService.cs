using Course.Shared.Dtos;
using Course.Shared.Services.Abstract;
using Course.Web.Models.Orders;
using Course.Web.Models.Payment;
using Course.Web.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Course.Web.Services.Concrede
{
    public class OrderService : IOrderService
    {

        private readonly HttpClient _httpClient;
        private readonly IPaymentService _paymentService;    
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;


        public OrderService(IPaymentService paymentService, HttpClient httpClient, IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _paymentService = paymentService;
            _httpClient = httpClient;
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }


        public async Task<OrderCreatedViewModel> CreateOrder(CheckOutModel checkoutInfoModel)
        {
            var basket = await _basketService.GetAsync();
            var payment = new PaymentInfoModel()
            {
                CardName = checkoutInfoModel.CardName,
                CardNumber = checkoutInfoModel.CardNumber,
                Expiration = checkoutInfoModel.Expiration,
                CVV = checkoutInfoModel.CVV,
                TotalPrice = basket.TotalPrice       
            };

            var receivePayment = await _paymentService.ReceivePayment(payment);
            if (!receivePayment)
            {
                return new OrderCreatedViewModel { Error = "Payment failed", IsSuccess = false };
            }

            var orderCreateModel = new OrderCreateModel()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new AddressCreateModel
                {
                    Province = checkoutInfoModel.Province,
                    District = checkoutInfoModel.District,
                    Line = checkoutInfoModel.Line,
                    Street = checkoutInfoModel.Street,
                    ZipCode = checkoutInfoModel.ZipCode

                }
            };

            basket.BasketItems.ForEach(x =>
            {
                new OrderItemCreateModel
                {
                    ProductId = x.CourseId,
                    PictureUrl = "",
                    Price = x.Price,
                    ProductName = x.CourseName
                };
            });

            var response = await _httpClient.PostAsJsonAsync<OrderCreateModel>("order", orderCreateModel);
            if (!response.IsSuccessStatusCode)
            {
                return new OrderCreatedViewModel()
                {
                    Error = "Order couldnt create",
                    IsSuccess = false,
                };
            }

            var orderCreatedViewModel = await response.Content.ReadFromJsonAsync<Response<OrderCreatedViewModel>>();

            orderCreatedViewModel.Data.IsSuccess = true;
            await _basketService.DeleteAsync();
            return orderCreatedViewModel.Data;

        }

        public async Task<List<OrderViewModel>> GetOrder()
        {
            var response = await _httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("order");
            return response.Data;
        }

        public async Task SuspendOrder(CheckOutModel checkoutInfoModel)
        {
            throw new NotImplementedException();
        }
    }
}
