using Store.G02.Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Abstractions.Baskets
{
    public interface IBasketService
    {
       Task<BasketDto> GetBasketAsync(string id);
       Task<BasketDto> CreateBasketAsync(BasketDto dto,TimeSpan duration);
     
       Task<bool> DeleteBasketAsync(string id); 

    }
}
 