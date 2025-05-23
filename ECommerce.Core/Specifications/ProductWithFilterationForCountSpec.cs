using ECommerce.Core.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Specifications
{
    public class ProductWithFilterationForCountSpec :Specification<Product>
    {
        public ProductWithFilterationForCountSpec(ProductSpecParams Params):base
            (p =>
             (string.IsNullOrEmpty(Params.Search) || p.name.ToLower().Contains(Params.Search))
            &&
            (!Params.TypeId.HasValue || p.ProductTypeId==Params.TypeId) 
            &&  
            (!Params.BrandId.HasValue || p.ProductBrandId ==Params.BrandId )) 
        {
            
        }
    }
}
