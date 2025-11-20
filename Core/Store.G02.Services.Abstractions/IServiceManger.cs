using Store.G02.Services.Abstractions.Auth;
using Store.G02.Services.Abstractions.Baskets;
using Store.G02.Services.Abstractions.Cache;
using Store.G02.Services.Abstractions.Orders;
using Store.G02.Services.Abstractions.Payments;
using Store.G02.Services.Abstractions.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Abstractions
{
    public interface IServiceManger
    {
         IProductService ProductService { get; }
         IBasketService BasketService { get; }
         ICacheService CacheService { get; }
         IAuthService AuthService { get; }
         IOrderService OrderService { get; }
         IPaymentService PaymentService { get; }
    }
}
