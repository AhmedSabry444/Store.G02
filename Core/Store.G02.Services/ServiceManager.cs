
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities.Identity;
using Store.G02.Services.Abstractions;
using Store.G02.Services.Abstractions.Auth;
using Store.G02.Services.Abstractions.Baskets;
using Store.G02.Services.Abstractions.Cache;
using Store.G02.Services.Abstractions.Orders;
using Store.G02.Services.Abstractions.Payments;
using Store.G02.Services.Abstractions.Products;
using Store.G02.Services.Auth;
using Store.G02.Services.Baskets;
using Store.G02.Services.Cache;
using Store.G02.Services.Orders;
using Store.G02.Services.Payments;
using Store.G02.Services.Products;
using Store.G02.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services
{
    public class ServiceManager(IUnitOfWork _unitOfWork,IMapper _mapper,IBasketRepository _basketRepository,ICacheRepository _cacheRepository,UserManager<AppUser> _userManager,IOptions<JwtOptions>_options,IConfiguration configuration) : IServiceManger
    {
        public IProductService ProductService { get; } = new ProductService(_unitOfWork, _mapper);
        public IBasketService BasketService { get; } = new BasketService(_basketRepository, _mapper);
        public ICacheService CacheService { get; } = new CacheService(_cacheRepository);

        public IAuthService AuthService { get; } = new AuthService(_userManager, _options,_mapper);

        public IOrderService OrderService { get; }=new OrderService(_unitOfWork,_mapper,_basketRepository) ;

        public IPaymentService PaymentService { get;} = new PaymentService(_basketRepository,_unitOfWork,configuration,_mapper);
    }
}
