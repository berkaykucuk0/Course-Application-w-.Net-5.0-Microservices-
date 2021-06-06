using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Services.Order.Application.Dtos
{
    public class OrderDto
    {

        //Entity class have id property so we write here
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public AddressDto Address { get; set; }

        public string BuyerId { get; set; }

        //We wrote list cuz this is dto
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
