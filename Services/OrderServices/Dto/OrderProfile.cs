using AutoMapper;
using Core.Entities.OrderEntities;
using Core.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OrderServices.Dto
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Address , AddressDto>().ReverseMap();
            CreateMap<AddressDto, ShippingAddress>().ReverseMap();
            CreateMap<Order , OrderResultDto>()
                .ForMember(dest=>dest.DeliveryMethod, optipn => optipn.MapFrom(src=>src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.ShippingPrice, optipn => optipn.MapFrom(src => src.DeliveryMethod.Price)).ReverseMap();
            CreateMap<OrderItem , OrderItemDto>()
                .ForMember(dest => dest.ProductItemId, optipn => optipn.MapFrom(src => src.ItemOrdered.ProductItemId))
                .ForMember(dest => dest.ProductName, optipn => optipn.MapFrom(src => src.ItemOrdered.ProductName))
                .ForMember(dest => dest.PictureUrl, optipn => optipn.MapFrom(src => src.ItemOrdered.PictureUrl))
                .ForMember(dest => dest.PictureUrl, optipn => optipn.MapFrom<OrderItemUrlResolver>()).ReverseMap();





        }
    }
}
