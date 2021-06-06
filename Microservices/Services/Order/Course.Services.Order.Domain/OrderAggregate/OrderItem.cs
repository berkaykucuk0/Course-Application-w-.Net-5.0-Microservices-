using Course.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Services.Order.Domain.OrderAggregate
{
    public class OrderItem:Entity
    {
        //This properties private set because  we don't want anyone else change it in other places.Only we can set these values from function in this class
        public string ProductId  { get; private set; }
        public string ProductName { get; private set; }
        public string PictureUrl { get; private set; }
        public Decimal Price { get; private set; }


        // OrderId is shadow property.Meaning, Ef core will create this value in database for one to many relationship but we wont write here this property.
        //  public string OrderId { get; private set; }
        public OrderItem()
        {

        }

        public OrderItem(string productId, string productName, string pictureUrl, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
            Price = price;
        }


        //We can set values in here , so we did private set 
        public void UpdateOrderItem(string productName, string pictureUrl, decimal price)
        {
            ProductName = productName;
            Price = price;
            PictureUrl = pictureUrl;
        }
    }
}
