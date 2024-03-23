using Core.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specifications
{
    public class OrderWithPaymentSpecifications : BaseSpecifications<Order>
    {
        public OrderWithPaymentSpecifications(string PaymentIntentId) : base(Order=>Order.PaymentIntentId == PaymentIntentId)
        {
        }
    }
}
