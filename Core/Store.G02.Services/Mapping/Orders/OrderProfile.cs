using AutoMapper;
using Store.G02.Domain.Entities.Orders;
using Store.G02.Services.Abstractions.Orders;
using Store.G02.Shared.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Mapping.Orders
{
    public class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            CreateMap<OrderAddress, OrderAddressDto>().ReverseMap();

            CreateMap<Order, OrderResponse>().ForMember(D=>D.DeliveryMethod,O=>O.MapFrom(S=>S.DeliveryMethod.ShortName))
                .ForMember(D => D.Total, O => O.MapFrom(S => S.GetTotal()));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(D => D.ProductId, O => O.MapFrom(S => S.Product.ProductId))
                .ForMember(D => D.ProductName, O => O.MapFrom(S => S.Product.ProductName))
                .ForMember(D => D.PictureUrl, O => O.MapFrom(S => S.Product.PictureUrl));

            CreateMap<DeliveryMethod, DeliveryMethodResponse>();
        }
    }
}
