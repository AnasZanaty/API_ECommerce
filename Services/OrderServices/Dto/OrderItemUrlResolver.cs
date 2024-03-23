using AutoMapper;
using Core.Entities;
using Core.Entities.OrderEntities;
using Microsoft.Extensions.Configuration;
using Services.ProductServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OrderServices.Dto
{
    
        public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
        {
            private readonly IConfiguration configuration;

            //to find the appsetting.json we have to inject the Iconfiguration interface
            public OrderItemUrlResolver(IConfiguration configuration)
            {
                this.configuration = configuration;
            }
            public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
            {
                if (!string.IsNullOrEmpty(source.ItemOrdered.PictureUrl))
                {
                    return configuration["BaseUrl"] + source.ItemOrdered.PictureUrl;
                }
                return null;
            }
        }
    
}
