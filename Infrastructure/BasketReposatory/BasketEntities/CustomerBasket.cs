using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BasketReposatory.BasketEntities
{
    public class CustomerBasket
    {
        public string Id { get; set; } //the Id is string as it will be taken as key in caching with Redis

        public List<BasketItem> BasketItems { get; set; }=new List<BasketItem>();


        public int ? DeliveryMethodId { get; set; }         //SEED OF DELIVERY


        public decimal shippingPrice { get; set; }

        public string? PaymentIntentId { get; set; }

        public string? ClientSecret { get; set; } //STRIPE GENERTATE clientsecred need to be saved so when we return to stripe can recognize the client which integrated with 

       



    }
}
