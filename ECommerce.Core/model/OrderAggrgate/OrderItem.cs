using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.model.OrderAggrgate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductItemOrder productItem, decimal price, int quantity)
        {
            ProductItem = productItem;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrder ProductItem { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
