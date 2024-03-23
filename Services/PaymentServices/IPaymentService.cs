using Services.BasketServices.Dto;
using Services.OrderServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PaymentServices
{
    public interface IPaymentService
    {
        Task<CustomerBasketDto> CreateOrUpdatePaymentIntentForExistingOrder(CustomerBasketDto basket);
        Task<CustomerBasketDto> CreateOrUpdatePaymentIntentForNewOrder(string basketId);

        Task<OrderResultDto> UpdateOrderPaymentSucceeded(string PaymentIntentId , string basketId);

        Task<OrderResultDto> UpdateOrderPaymentFailed(string PaymentIntentId);

    }
}
