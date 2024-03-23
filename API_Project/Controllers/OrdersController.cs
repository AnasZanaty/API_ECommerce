using Core.Entities;
using Demo.HandleResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.OrderServices;
using Services.OrderServices.Dto;
using System.Security.Claims;

namespace Demo.Controllers
{
    [Authorize]

    public class OrdersController : BaseController

    {
        private readonly IorderService orderService;

        public OrdersController(IorderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<OrderResultDto>> CreateOrderAsync(OrderDto orderDto)
        {

            var order = await orderService.CreateOrderAsync(orderDto);
            if (order is null)
            {
                return BadRequest(new ApiResponse(400, "Error While Creating Order"));

            }

            return Ok(order);

        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderResultDto>>> GetAllOrderforUserAsync()
        {

            var email = User?.FindFirstValue(ClaimTypes.Email);
            var orders = await orderService.GetAllOrderforUserAsync(email);
            if (orders is { Count: <= 0 })
                return Ok((new ApiResponse(200, "You dont have any orders yet")));
            return Ok(orders);

        }

        [HttpGet]
        public async Task<ActionResult<OrderResultDto>> GetOrderIdAsync(int Id)
        {
            var email = User?.FindFirstValue(ClaimTypes.Email);
            var order = await orderService.GetOrderIdAsync(Id, email);
            if (order is null)
            {
                return Ok(new ApiResponse(400, "There's no order with this id "));

            }

            return Ok(order);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryMethod>>> GetAllDeliveryMethodAsync()

        {
            var order = await orderService.GetAllDeliveryMethodAsync();
       
            return Ok(order);

        }

    }
}
