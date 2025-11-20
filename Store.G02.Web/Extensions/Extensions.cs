using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities.Identity;
using Store.G02.Persistence;
using Store.G02.Persistence.Identity.Contexts;
using Store.G02.Services;
using Store.G02.Shared;
using Store.G02.Shared.ErrorModels;
using Store.G02.Web.Middlewares;
using System.Text;

namespace Store.G02.Web.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddAllServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.

            services.AddWebSercvices();

            services.AddInfrastructureServices(configuration);

            services.AddApplicationServices(configuration);

            services.ConfigureApiBehaviorOptions();

            services.AddIdentitySercvices();
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            services.AddAuthenticationService(configuration);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });



            return services;
        }

        private static IServiceCollection ConfigureApiBehaviorOptions(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {


                config.InvalidModelStateResponseFactory = (actionContext) =>

                {
                    var errors = actionContext.ModelState.Where(M => M.Value.Errors.Any()).Select(M => new ValidationError()

                    {
                        Field = M.Key,
                        Errors = M.Value.Errors.Select(E => E.ErrorMessage)
                    }).ToList();



                    var response = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            });
            return services;
        }
        private static IServiceCollection AddAuthenticationService(this IServiceCollection services , IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey))

                };
            });
            return services;
        }

        private static IServiceCollection AddWebSercvices(this IServiceCollection services)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        private static IServiceCollection AddIdentitySercvices(this IServiceCollection services)
        {
            services.AddIdentityCore<AppUser>(options => { options.User.RequireUniqueEmail = true; }).AddRoles<IdentityRole>().AddEntityFrameworkStores<IdentityStoreDbContext>();


            return services;
        }







        public static async Task<WebApplication> ConfigureMiddlewares(this WebApplication app)
        {
            #region Initialize Db
            await app.SeedData();
            #endregion



            app.UseGlobalErrorHandling();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthorization();
            app.UseAuthentication();


            app.MapControllers();

            return app;
        }

        private static async Task<WebApplication> SeedData(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();
            await dbInitializer.InitializeIdentityAsync();
            return app;
        }


        private static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return app;
        }
    }
}
