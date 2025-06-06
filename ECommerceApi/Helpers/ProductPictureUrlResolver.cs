﻿using AutoMapper;
using ECommerce.Core.model;
using ECommerceApi.DTOs;

namespace ECommerceApi.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.img))
            {
                return $"{_configuration["ApiBaseUrl"]}images/products/{source.img}";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
