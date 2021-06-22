using Course.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Services.Order.Domain.OrderAggregate
{
    //LOOK HEREEEEEE
    //EF Core features
    // -- Owned Types
    // -- Shadow Property
    // -- Backing Field
    public class Order: Entity,IAggregateRoot
    {
        //This properties private set because  we don't want anyone else change it in other places.Only we can set these values from function in this class
        public DateTime CreatedDate { get; private set; }

        public Address Address { get; private set; }

        public string BuyerId { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
        public Order()
        {

        }

        public Order(Address address, string buyerId)
        {
            Address = address;
            BuyerId = buyerId;
            CreatedDate = DateTime.Now;
            _orderItems = new List<OrderItem>();
        }

        //Order item adding function
        public void AddOrderItem(string productId, string productName, decimal price, string pictureUrl)
        {
            var existProduct = _orderItems.Any(x => x.ProductId == productId);
            if (!existProduct)
            {
                OrderItem newOrderItem = new (productId, productName, pictureUrl, price);
                _orderItems.Add(newOrderItem);
            }

        }


        //Helper property for Total Price
        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
    }
}
