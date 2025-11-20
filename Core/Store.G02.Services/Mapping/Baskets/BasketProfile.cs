using AutoMapper;
using Store.G02.Domain.Entities.Baskets;
using Store.G02.Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Mapping.Baskets
{
    public class BasketProfile : Profile
    {
        public BasketProfile() {
        
            CreateMap<CustomerBasket,BasketDto>().ReverseMap();
            CreateMap<BasketItem,BasketItemDto>().ReverseMap();

        }
    }
}
