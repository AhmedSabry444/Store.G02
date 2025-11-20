using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities.Identity;
using Store.G02.Domain.Entities.Orders;
using Store.G02.Domain.Entities.Products;
using Store.G02.Persistence.Data.Contexts;
using Store.G02.Persistence.Identity.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.G02.Persistence
{
    public class DbInitializer(StoreDbContext _context, IdentityStoreDbContext _identitycontext, UserManager<AppUser> _userManager, RoleManager<IdentityRole> _roleManager) : IDbInitializer
    {


        public async Task InitializeAsync()
        {
            if (_context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any())
            {

                await _context.Database.MigrateAsync();
            }



            if (!_context.DeliveryMethod.Any())
            {
                var deliveryData = await File.ReadAllTextAsync(@"..\Infrastructure\Store.G02.Persistence\Data\DataSeeding\delivery.json");

                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);


                if (deliveryMethods is not null && deliveryMethods.Count > 0)
                {
                    await _context.DeliveryMethod.AddRangeAsync(deliveryMethods);
                }

            }


            if (!_context.ProductBrands.Any())
            {
                var brandsdata = await File.ReadAllTextAsync(@"..\Infrastructure\Store.G02.Persistence\Data\DataSeeding\brands.json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsdata);


                if (brands is not null && brands.Count > 0)
                {
                    await _context.ProductBrands.AddRangeAsync(brands);
                }

            }
         
            
            
            if (!_context.ProductTypes.Any())
            {
                var typesdata = await File.ReadAllTextAsync(@"..\Infrastructure\Store.G02.Persistence\Data\DataSeeding\types.json");

                var types = JsonSerializer.Deserialize<List<ProductType>>(typesdata);


                if (types is not null && types.Count > 0)
                {
                    await _context.ProductTypes.AddRangeAsync(types);
                }

            }
         
            
            if (!_context.Products.Any())
            {
                var productsdata = await File.ReadAllTextAsync(@"..\Infrastructure\Store.G02.Persistence\Data\DataSeeding\products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productsdata);


                if (products is not null && products.Count > 0)
                {
                    await _context.Products.AddRangeAsync(products);
                }

            }

            await _context.SaveChangesAsync();

        }

        public async Task InitializeIdentityAsync()
        {
            if (_identitycontext.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any())
            {

                await _identitycontext.Database.MigrateAsync();
            }

            if (!_identitycontext.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = "SuperAdmin" });
                await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });

            }


            if (!_identitycontext.Users.Any())
            {
                var superAdmin = new AppUser()
                {
                    UserName = "SuperAdmin",
                    Email = "SuperAdmin@gmail.com",
                    DisplayName = "SuperAdmin",
                    PhoneNumber = "01010043702"
                };
                var admin = new AppUser()
                {
                    UserName = "Admin",
                    Email = "Admin@gmail.com",
                    DisplayName = "Admin",
                    PhoneNumber = "01010043702"
                };
                await _userManager.CreateAsync(superAdmin, "SuperAdmin@123");   
                await _userManager.CreateAsync(admin, "Admin@123");   

                await _userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
                await _userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
