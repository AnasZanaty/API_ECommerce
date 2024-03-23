using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderEntities
{
    public class ShippingAddress
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string street { get; set; }

        public string city { get; set; }

        public string state { get; set; }


        public string zipcode { get; set; }

    }
}
