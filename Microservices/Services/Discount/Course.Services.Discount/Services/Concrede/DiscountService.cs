using Course.Services.Discount.Services.Abstract;
using Course.Shared.Dtos;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Discount.Services.Concrede
{
    public class DiscountService : IDiscountService
    {
        //Get database with IConfiguration
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;

            //we mapped _dbConnection in appsettings.json PostgreSql json field
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var status = await _dbConnection.ExecuteAsync("Delete from discount where id=@Id", new { Id = id });
            return status > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount not found",404);
        }

        public async Task<Response<List<Entities.Discount>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Entities.Discount>("SELECT * FROM discount");
            return Response<List<Entities.Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<Entities.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discounts = await _dbConnection.QueryAsync<Entities.Discount>("select * from discount where userid=@UserId and code=@Code", new { UserId = userId, Code = code });

            var hasDiscount = discounts.FirstOrDefault();

            if (hasDiscount == null)
            {
                return Response<Entities.Discount>.Fail("Discount not found", 404);
            }

            return Response<Entities.Discount>.Success(hasDiscount, 200);
        }

        public async Task<Response<Entities.Discount>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Entities.Discount>("select from discount where id=@Id", new { Id = id })).SingleOrDefault();
            if (discount==null)
            {
                return Response<Entities.Discount>.Fail("Discount not found", 404);
            }

            return Response<Entities.Discount>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> Save(Entities.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("INSERT INTO discount (userid,rate,code) VALUES(@UserId,@Rate,@Code)", discount);
            if (status>0)
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("Discount not found", 404);
        }

        public async Task<Response<NoContent>> Update(Entities.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("update discount set userId=@UserId,code=@Code, rate=@Rate where id=@Id",
                                                         new { Id = discount.Id, UserId = discount.UserId, Code = discount.Code, Rate = discount.Rate });
            if (status>0)
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("Discount not found", 404);
        }
    }
}
