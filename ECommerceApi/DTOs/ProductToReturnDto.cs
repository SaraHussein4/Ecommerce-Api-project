using ECommerce.Core.model;

namespace ECommerceApi.DTOs
{
    public class ProductToReturnDto
    {
        public int id {  get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public decimal price { get; set; }
        public string? img { get; set; }

        public int ProductBrandId { get; set; }
        public int ProductTypeId { get; set; }

        public string ProductBrand { get; set; }

        public string ProductType { get; set; }
    }
}
