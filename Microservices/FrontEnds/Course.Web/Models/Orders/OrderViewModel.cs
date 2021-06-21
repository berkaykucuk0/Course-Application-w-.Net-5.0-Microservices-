using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Web.Models.Orders
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }

        //Not received because there is no need for an address field in my payment history
        // public AddressDto Address { get; set; }

        public string BuyerId { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; }
    }
}
