using Core.Entities;
using Core.Entities.OrderEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base (options) 
            { 
               
            }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //to find the classes which implement the IEntityConfiguration interface
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }



        public DbSet <Product> Products { get; set; }

        public DbSet <ProductType> productTypes { get; set; }

        public DbSet <ProductBrand> productBrands { get; set; }

        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }


    }
}
