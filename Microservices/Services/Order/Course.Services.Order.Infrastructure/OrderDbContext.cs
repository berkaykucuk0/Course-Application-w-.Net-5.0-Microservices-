using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Services.Order.Infrastructure
{
    public class OrderDbContext:DbContext
    {
        public const string DEFAULT_SCHEMA = "ordering";
        public OrderDbContext(DbContextOptions<OrderDbContext> options) :base(options)
        {

        }

        public DbSet<Domain.OrderAggregate.Order> Order { get; set; }
        public DbSet<Domain.OrderAggregate.OrderItem> OrderItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Set table names
            modelBuilder.Entity<Domain.OrderAggregate.Order>().ToTable("Order", DEFAULT_SCHEMA);
            modelBuilder.Entity<Domain.OrderAggregate.OrderItem>().ToTable("OrderItem", DEFAULT_SCHEMA);


            //max 12 character and 2 characters after the dot (for ex: 99999999999999.99)
            modelBuilder.Entity<Domain.OrderAggregate.OrderItem>().Property(x => x.Price).HasColumnType("decimal(18,2)");
            base.OnModelCreating(modelBuilder);
        }

    }
}
