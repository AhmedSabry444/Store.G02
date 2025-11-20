using AutoMapper;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities.Baskets;
using Store.G02.Domain.Exceptions.NotFound;
using Store.G02.Domain.Exceptions.BadRequest;
using Store.G02.Services.Abstractions.Baskets;
using Store.G02.Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Baskets
{
    public class BasketService(IBasketRepository _basketRepository,IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateBasketAsync(BasketDto dto, TimeSpan duration)
        {
           var basket = _mapper.Map<CustomerBasket>(dto);
            var result = await _basketRepository.CreateBasketAsync(basket, duration);
            if (result is null) throw new CreateOrUpdateBadRequestException();
            return _mapper.Map<BasketDto>(result);
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
          var flag = await  _basketRepository.DeleteBasketAsync(id);
            if (flag) throw new DeleteBasketBadRequestException();
            return flag;
        }

        public async Task<BasketDto> GetBasketAsync(string id)
        {
         var basket = await   _basketRepository.GetBasketAsync(id);
            if(basket is null) throw new BasketNotFoundException(id) ;
            var result =  _mapper.Map<BasketDto>(basket);
            return result;
        }
    }
}
