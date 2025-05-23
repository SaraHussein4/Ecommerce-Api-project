using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Specifications
{
    public class ProductSpecParams
    {
        public string? Sort { get; set; }
        public int? TypeId { get; set; }

        public int? BrandId { get; set; }

        private int pageSize=10;

     

        public int PageIndex
        {
            get => pageIndex;
            set => pageIndex = (value < 1) ? 1 : value;
        }
        private int pageIndex = 1;

        public int PageSize {

            get
            {
                return pageSize;
            }

            set
            {
                pageSize = value>10?10:value;
            } 
        
        }

        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }

    }
}
