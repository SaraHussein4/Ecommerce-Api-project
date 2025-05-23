using ECommerce.Core.model;
using ECommerce.Core.Repositories;
using ECommerce.Repository.Data;
using ECommerce.Repository;
using ECommerceApi.Helpers;
using Microsoft.AspNetCore.Identity;
using ECommerce.Core.Identity;
using ECommerce.Repository.Identity;
using ECommerce.Core;
using ECommerce.Core.Service;
using ECommerce.Service;

namespace ECommerceApi.Extensions
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services) 
        
        {
            Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            Services.AddScoped<IGenericRepository<ProductBrand>, GenericRepository<ProductBrand>>();
            Services.AddScoped<IGenericRepository<ProductType>, GenericRepository<ProductType>>();
            Services.AddScoped<IBasketRepository, BasketRepository>();  

          Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IOrderService, OrderService>();
            Services.AddAutoMapper(typeof(MappingProfiles));

            return Services;

        }
    }
}
