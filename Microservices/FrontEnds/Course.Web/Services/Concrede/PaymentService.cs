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
    public class PaymentService : IPaymentService
    {

        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ReceivePayment(PaymentInfoModel  paymentInfoModel)
        {
            var response = await _httpClient.PostAsJsonAsync<PaymentInfoModel>("fakepayment", paymentInfoModel);

            return response.IsSuccessStatusCode;
        }
    }
}
