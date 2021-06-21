using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Web.Models.Orders
{
    public class OrderCreateModel
    {
        public OrderCreateModel()
        {
            OrderItems = new List<OrderItemCreateModel>();
        }

        public string BuyerId { get; set; }

        public List<OrderItemCreateModel> OrderItems { get; set; }

        public AddressCreateModel Address { get; set; }
    }
}
