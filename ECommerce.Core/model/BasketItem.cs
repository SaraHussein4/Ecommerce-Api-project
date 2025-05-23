using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.model
{
    public class BasketItem
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        public string Brand { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }

    }
}
