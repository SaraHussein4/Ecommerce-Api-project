using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.model
{
    public class Product : BaseEntity
    {
        public string name { get; set; }
        public string? description { get; set; }
        public decimal price { get; set; }
        public string? img {  get; set; }

        public int ProductBrandId { get; set; }
        public int ProductTypeId { get; set; }

        public ProductBrand ProductBrand { get; set; }

        public ProductType ProductType { get; set; }
    }
}
