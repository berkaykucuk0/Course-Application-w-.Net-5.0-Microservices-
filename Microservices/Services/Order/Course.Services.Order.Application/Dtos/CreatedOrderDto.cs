using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Services.Order.Application.Dtos
{
    //we created this dto for return the id of the created order
    public class CreatedOrderDto
    {
        public int OrderId { get; set; }
    }
}
