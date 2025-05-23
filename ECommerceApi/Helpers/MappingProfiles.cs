using AutoMapper;
using ECommerce.Core.Identity;
using ECommerce.Core.model;
using ECommerceApi.DTOs;
using ECommerce.Core.model.OrderAggrgate;

namespace ECommerceApi.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.name))
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.name))
                .ForMember(d => d.img, O =>O.MapFrom<ProductPictureUrlResolver>());
            CreateMap<ECommerce.Core.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<AddressDto, ECommerce.Core.model.OrderAggrgate.Address>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d=>d.Deliverymethod,O=>O.MapFrom(S=>S.Deliverymethod.ShortName))
                .ForMember(d=>d.DeliveryMethodCost,O=>O.MapFrom(S=>S.Deliverymethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, O => O.MapFrom(S => S.ProductItem.ProductId))
                .ForMember(d => d.ProductName, O => O.MapFrom(S => S.ProductItem.ProductName))
                //.ForMember(d => d.PictureUrl, O => O.MapFrom(S => S.ProductItem.PictureUrl))
                .ForMember(d => d.PictureUrl, O => O.MapFrom<OrderItemPictureUrlResolver>());
            CreateMap<ProductUpdateDto, Product>();

        }


    }
}
