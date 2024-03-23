using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderEntities
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {
                
        }

        public OrderItem(decimal price, int quantity, ProductItemOrder itemOrdered)
        {
            Price = price;
            Quantity = quantity;
            ItemOrdered = itemOrdered;
        }

        public decimal Price { get; set; }
        public int Quantity { get; set; }

        //Not a relation 
        public ProductItemOrder ItemOrdered { get; set; }
    }
}
