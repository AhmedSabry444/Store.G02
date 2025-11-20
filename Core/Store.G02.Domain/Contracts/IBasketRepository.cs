using Store.G02.Domain.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string id);
        Task<CustomerBasket?> CreateBasketAsync(CustomerBasket basket,TimeSpan duration);
        Task<bool> DeleteBasketAsync(string id);
}
}
