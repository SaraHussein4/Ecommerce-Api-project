using ECommerce.Core.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecification :Specification<Product>
    {
        public ProductWithBrandAndTypeSpecification(ProductSpecParams Params) : base
            (p =>  
            (string.IsNullOrEmpty(Params.Search) || p.name.ToLower().Contains(Params.Search))
            &&
            (!Params.TypeId.HasValue || p.ProductTypeId==Params.TypeId) 
            &&  
            (!Params.BrandId.HasValue || p.ProductBrandId ==Params.BrandId )) 
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
            if(!string.IsNullOrEmpty(Params.Sort))
            {
                switch (Params.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.price);
                    break;
                    case "PriceDesc":
                        AddOrderByDesc(p => p.price);
                        break;
                    default:
                        AddOrderBy(p => p.name);
                        break;
                }
            }

            ApplyPagination(Params.PageSize * (Params.PageIndex - 1), Params.PageSize);
        }
        public ProductWithBrandAndTypeSpecification(int id):base(p=>p.id==id)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);

        }
    }
}
