using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Dto
{
    public class AddShippingAddressRequest
    {
        [Required]
        public string ContactPersonsName { get; set; }

        [Required]
        public string Address { get; set; }

        public string City { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string AlternatePhoneNumber { get; set; }

        public string Landmark { get; set; }
    }
}