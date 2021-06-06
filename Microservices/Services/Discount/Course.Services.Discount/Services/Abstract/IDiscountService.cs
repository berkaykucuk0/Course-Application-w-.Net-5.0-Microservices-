using Course.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Discount.Services.Abstract
{

    /* We should do this with DiscountDto, DiscountUpdateDto,DiscountCreateDto (etc) 
       instead of entity Discount but we did with Discount entity cuz 
       our microservice have small code blocks */
    public interface IDiscountService
    {
        Task<Response<List<Entities.Discount>>> GetAll();
        Task<Response<Entities.Discount>> GetById(int id);
        Task<Response<NoContent>> Save(Entities.Discount discount);

        Task<Response<NoContent>> Update(Entities.Discount discount);

        Task<Response<NoContent>> Delete(int id);

        Task<Response<Entities.Discount>> GetByCodeAndUserId(string code, string userId);
    }
}
