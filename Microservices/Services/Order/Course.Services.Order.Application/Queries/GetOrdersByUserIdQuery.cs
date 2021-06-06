﻿using Course.Services.Order.Application.Dtos;
using Course.Shared.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Services.Order.Application.Queries
{
    //Queries are used for Get methods.
    public class GetOrdersByUserIdQuery:IRequest<Response<List<OrderDto>>>
    {
        public string UserId { get; set; }
    }
}
