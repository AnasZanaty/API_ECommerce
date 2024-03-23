using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.BasketServices;
using Services.BasketServices.Dto;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : BaseController
    {
        private readonly IBasketService basketService;

        public BasketController(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto>>GetBsketById(string id)
        =>
           Ok( await basketService.GetBasketAsync(id));

        [HttpPost]

        public async Task<ActionResult<CustomerBasketDto>> UpdateBsketAsync(CustomerBasketDto basket)

            => Ok(await basketService.UpdateBasketAsync(basket));

        [HttpDelete]
        public async Task DeleteBasketAsync  (string id)
            => Ok(await basketService.DeleteBasketAsync(id));

        
    }
}
