using Course.Services.Order.Application.Dtos;
using Course.Services.Order.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace Course.Services.Order.Application.Mapping
{
    //Mapping Process
    public class CustomMapping:Profile
    {
        public CustomMapping()
        {
            CreateMap<Domain.OrderAggregate.Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
