using AutoMapper;
using ECommerce.Core.model.OrderAggrgate;
using ECommerceApi.DTOs;

namespace ECommerceApi.Helpers
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductItem.PictureUrl))
            {
                return $"{_configuration["ApiBaseUrl"]}images/products/{source.ProductItem.PictureUrl}";
            }
            else
            {
                return  "images/products/mouse.jpg";
            }
        }
    }
}
