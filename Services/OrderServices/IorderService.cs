using Core.Entities;
using Services.OrderServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OrderServices
{
    public interface IorderService
    {
        Task<OrderResultDto> CreateOrderAsync(OrderDto orderDto);
        Task<IReadOnlyList<OrderResultDto>> GetAllOrderforUserAsync(string buyerEmail);

        Task<OrderResultDto> GetOrderIdAsync(int Id, string buyerEmail);

        Task<IEnumerable<DeliveryMethod>>GetAllDeliveryMethodAsync ();

    }
}
