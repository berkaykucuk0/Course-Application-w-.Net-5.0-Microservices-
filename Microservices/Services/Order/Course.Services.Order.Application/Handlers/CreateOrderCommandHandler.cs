using Course.Services.Order.Application.Commands;
using Course.Services.Order.Application.Dtos;
using Course.Services.Order.Domain.OrderAggregate;
using Course.Services.Order.Infrastructure;
using Course.Shared.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Course.Services.Order.Application.Handlers
{
    // commandhandler for CreateOrderCommand
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
    {
        private readonly OrderDbContext _context;

        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }
        public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            //create new address
            Address address = new(request.Address.Province,
                                  request.Address.District,
                                  request.Address.Street,
                                  request.Address.ZipCode,
                                  request.Address.Line);

            //create new order
            Domain.OrderAggregate.Order newOrder = new(address, request.BuyerId);

            //add order items to order
            foreach (var orderItem in request.OrderItems)
            {
                newOrder.AddOrderItem(orderItem.ProductId, orderItem.ProductName, orderItem.Price, orderItem.PictureUrl);
            }
            await _context.Order.AddAsync(newOrder);
            await _context.SaveChangesAsync();

            //return success and OK() with CreatedOrderDto
            return Response<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = newOrder.Id }, 200);

        }
    }
}
