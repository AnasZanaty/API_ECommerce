using AutoMapper;
using Infrastructure.BasketReposatory;
using Infrastructure.BasketReposatory.BasketEntities;
using Services.BasketServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.BasketServices
{
    public class BasketService : IBasketService
    {
        private readonly IBasketReposatory basketReposatory;
        private readonly IMapper mapper;

        public BasketService(IBasketReposatory basketReposatory , IMapper mapper)
        {
            this.basketReposatory = basketReposatory;
            this.mapper = mapper;
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        => await basketReposatory.DeleteBasketAsync(basketId);

        public async Task<CustomerBasketDto> GetBasketAsync(string basketId)
        {
            var basket = await basketReposatory.GetBasketAsync(basketId);
            var mappedBasket = mapper.Map<CustomerBasketDto>(basket);
            return mappedBasket?? new CustomerBasketDto() ;
        }

        public async Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basket)
        {
            var customerBasket = mapper.Map<CustomerBasket>(basket);
            var updatedBasket = await basketReposatory.UpdateBasketAsync(customerBasket);
            var customerbasketMapped = mapper.Map<CustomerBasketDto>(updatedBasket);
            return customerbasketMapped;

        }
    }
}
