using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.DTOs
{
    public class AddressDto
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
    }
}
