using Core.Entities;
using Core.Entities.OrderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(Order => Order.shippingAddress, x => x.WithOwner()); //it means that order is the owner of shipping address so the attribute of the shipping address will be added to Order table 
            builder.HasMany(order=>order.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
