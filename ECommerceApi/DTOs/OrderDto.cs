using ECommerce.Core.model.OrderAggrgate;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.DTOs
{
    public class OrderDto
    {
        [Required]
        public string BasketId { get; set; }
        public int DeliverymethodId { get; set; }
        public AddressDto ShippingAddress { get; set; }
    }
}
