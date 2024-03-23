using AutoMapper;
using Core.Entities;
using Core.Entities.OrderEntities;
using Infrastructure.Interfaces;
using Infrastructure.Specifications;
using Microsoft.Extensions.Configuration;
using Services.BasketServices;
using Services.BasketServices.Dto;
using Services.OrderServices.Dto;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Core.Entities.Product;

namespace Services.PaymentServices
{
    public class PaymentServices : IPaymentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasketService basketService;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public PaymentServices(IUnitOfWork unitOfWork , IBasketService basketService , IConfiguration configuration , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.basketService = basketService;
            this.configuration = configuration;
            this.mapper = mapper;
        }
        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntentForExistingOrder(CustomerBasketDto? basket)
        {
            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];

            if (basket == null)
                return null;

            var shippingPrice = 0m;
            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
                shippingPrice = deliveryMethod.Price;
            }
            foreach (var item in basket.BasketItems)
            {
                var productitem = await unitOfWork.Repository<Product>().GetByIdAsync(item.Id); //make sure that the price in basket is the same price in product
                if(item.Price != productitem.Price)
                    item.Price = productitem.Price;
            }
            var service = new PaymentIntentService();
            PaymentIntent intent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId)) //don't have payymentIntentId so we create it
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.shippingPrice * 100 + (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100)),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                intent = await service.CreateAsync(options); //paymentitent will be created and clientsecret
                basket.PaymentIntentId= intent.Id;
                basket.ClientSecret= intent.ClientSecret;
            }
            else
            {
                //PAYMENTINTENT IS ALREADY FOUND
                var options = new PaymentIntentUpdateOptions
                {
                    //amount the only thing that can be changed to order
                    Amount = (long)basket.shippingPrice * 100 + (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100)),
           
                };
                intent = await service.UpdateAsync(basket.PaymentIntentId , options); //paymentitent will be created and clientsecret
       
            }

            await basketService.UpdateBasketAsync(basket);
            return basket;
        }

       public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntentForNewOrder(string basketId)
        {
            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
            
            var basket = await basketService.GetBasketAsync(basketId);

            if (basket == null)
                return null;

            var shippingPrice = 0m;
            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
                shippingPrice = deliveryMethod.Price;
            }
            foreach (var item in basket.BasketItems)
            {
                var productitem = await unitOfWork.Repository<Product>().GetByIdAsync(item.Id); //make sure that the price in basket is the same price in product
                if (item.Price != productitem.Price)
                    item.Price = productitem.Price;
            }
            var service = new PaymentIntentService();
            PaymentIntent intent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId)) //don't have payymentIntentId so we create it
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.shippingPrice * 100 + (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100)),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                intent = await service.CreateAsync(options); //paymentitent will be created and clientsecret
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                //PAYMENTINTENT IS ALREADY FOUND
                var options = new PaymentIntentUpdateOptions
                {
                    //amount the only thing that can be changed to order
                    Amount = (long)basket.shippingPrice * 100 + (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100)),

                };
                intent = await service.UpdateAsync(basket.PaymentIntentId, options); //paymentitent will be created and clientsecret

            }

            await basketService.UpdateBasketAsync(basket);
            return basket;
        }


        public async Task<OrderResultDto> UpdateOrderPaymentFailed(string PaymentIntentId)
        {
            var specs = new OrderWithPaymentSpecifications(PaymentIntentId);
            var order = await unitOfWork.Repository<Order>().GetEntityWithSpecificationAsync(specs);

            if (order == null)
            {
                return null;
            }
            order.OrderStatus = OrderStatus.PaymentFailed;
            unitOfWork.Repository<Order>().Update(order);
            await unitOfWork.Complete();

            var mappedOrder= mapper.Map<OrderResultDto>(order);

            return mappedOrder;



        }

        public async Task<OrderResultDto> UpdateOrderPaymentSucceeded(string PaymentIntentId , string basketId)
        {
            var specs = new OrderWithPaymentSpecifications(PaymentIntentId);
            var order = await unitOfWork.Repository<Order>().GetEntityWithSpecificationAsync(specs);

            if (order == null)
            {
                return null;
            }
            order.OrderStatus = OrderStatus.PaymentReceived;
            unitOfWork.Repository<Order>().Update(order);
            await basketService.DeleteBasketAsync(basketId);
            await unitOfWork.Complete();

            await basketService.DeleteBasketAsync(PaymentIntentId);


            var mappedOrder = mapper.Map<OrderResultDto>(order);

            return mappedOrder;
        }

      
    }
}
