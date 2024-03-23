using AutoMapper;
using Core.Entities;
using Core.Entities.OrderEntities;
using Infrastructure.BasketReposatory;
using Infrastructure.Interfaces;
using Infrastructure.Specifications;
using Services.BasketServices;
using Services.BasketServices.Dto;
using Services.OrderServices.Dto;
using Services.PaymentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OrderServices
{
    public class OrderService : IorderService
    {
        private readonly IBasketService basketService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IPaymentService paymentService;

        public OrderService(IBasketService basketService , IUnitOfWork unitOfWork, IMapper mapper ,IPaymentService paymentService)
        {
            this.basketService = basketService;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.paymentService = paymentService;
        }
        public async Task<OrderResultDto> CreateOrderAsync(OrderDto orderDto)
        {
            //Get Basket
            var Basket = await basketService.GetBasketAsync(orderDto.BasketId); //returns customerBasketDto

            if (Basket is null)
                return null;

            //Fill OrderItems from Basket Item
            var OrderItems = new List<OrderItemDto>();
            foreach (var item in Basket.BasketItems)
            {
                var ProductItem = await unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrder(ProductItem.Id, ProductItem.Name, ProductItem.PictureUrl);
                var orderItem = new OrderItem(ProductItem.Price, item.Quantity, itemOrdered);
                var mappedOrderItem = mapper.Map<OrderItemDto>(orderItem);

                OrderItems.Add(mappedOrderItem);

            }

            //GET delivery method

            var deliveryMethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(orderDto.DeliveryMethodId);

            //calulate subtotal

            var subTotal = OrderItems.Sum(item => item.Price * item.Quantity);

            //Check if order exist

            var specs = new OrderWithPaymentSpecifications(Basket.PaymentIntentId);
            var existingorder = await unitOfWork.Repository<Order>().GetEntityWithSpecificationAsync(specs);
            CustomerBasketDto customerBasket= new CustomerBasketDto();
            if (existingorder != null)
            {
                unitOfWork.Repository<Order>().Delete(existingorder);
                await paymentService.CreateOrUpdatePaymentIntentForExistingOrder(Basket);
            }

            else
            {
                customerBasket = await paymentService.CreateOrUpdatePaymentIntentForNewOrder(Basket.Id);

            }

            //create order
            var mappedShippingAddress = mapper.Map<ShippingAddress>(orderDto.ShippingAddress);
            var mappedorderitems = mapper.Map<List<OrderItem>>(OrderItems);


            var order = new Order(orderDto.BuyerEmail, mappedShippingAddress, deliveryMethod, mappedorderitems, subTotal,Basket.PaymentIntentId ?? customerBasket.PaymentIntentId);

            await unitOfWork.Repository<Order>().Add(order);

            await unitOfWork.Complete(); //complete returns savechanges

            //delete basket

            //await basketService.DeleteBasketAsync(orderDto.BasketId);

            var mappedorder =  mapper.Map<OrderResultDto>(order);
            return mappedorder;


        }

        public async Task<IEnumerable<DeliveryMethod>> GetAllDeliveryMethodAsync()
         => await unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
        public async Task<IReadOnlyList<OrderResultDto>> GetAllOrderforUserAsync(string buyerEmail)
        {
            var specs = new OrderWithItemsSpecifications(buyerEmail);
            var orders= await unitOfWork.Repository<Order>().GetAllWithSpecificationsAsync(specs);
            var mappedorders = mapper.Map<List<OrderResultDto>>(orders);
            return mappedorders;
        }

        public async Task<OrderResultDto> GetOrderIdAsync(int Id, string buyerEmail)
        {
            var specs = new OrderWithItemsSpecifications(Id , buyerEmail);
            var order = await unitOfWork.Repository<Order>().GetEntityWithSpecificationAsync(specs);

            var mappedorder = mapper.Map<OrderResultDto>(order);
            return mappedorder;
        }
    }
}
