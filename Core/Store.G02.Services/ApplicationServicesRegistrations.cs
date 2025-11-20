using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.G02.Services.Abstractions;
using Store.G02.Services.Mapping.Auth;
using Store.G02.Services.Mapping.Baskets;
using Store.G02.Services.Mapping.Orders;
using Store.G02.Services.Mapping.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services
{
    public static class ApplicationServicesRegistrations
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IServiceManger, ServiceManager>();
            services.AddAutoMapper(M => M.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(M => M.AddProfile(new BasketProfile()));
            services.AddAutoMapper(M => M.AddProfile(new OrderProfile()));
            services.AddAutoMapper(M => M.AddProfile(new AuthProfile()));
            return services;
        }
    }
}
