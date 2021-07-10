using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Dto
{
    public class ShippingAddressResponse
    {
        public int ShippingAddressId { get; set; }
        public string ContactPersonsName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternatePhoneNumber { get; set; }
        public string Landmark { get; set; }
        public int CustomerId { get; set; }
    }
}