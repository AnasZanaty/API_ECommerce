﻿using Infrastructure.BasketReposatory.BasketEntities;
using Services.BasketServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BasketServices
{
    public interface IBasketService
    {
        Task<CustomerBasketDto> GetBasketAsync(string basketId);

        Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basket);

        Task<bool> DeleteBasketAsync(string basketId);
    }
}
