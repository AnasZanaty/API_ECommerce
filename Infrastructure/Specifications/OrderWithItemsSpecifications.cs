using Core.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specifications
{
    public class OrderWithItemsSpecifications : BaseSpecifications<Order>
    {
        public OrderWithItemsSpecifications(string email) : base (Order=> Order.BuyerEmail == email)
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeliveryMethod);
            AddOrderByDescending(order => order.OrderDate);


        }
        public OrderWithItemsSpecifications(int id , string email) : base(Order => Order.BuyerEmail == email && Order.Id==id)
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeliveryMethod);
      


        }

    }
}
