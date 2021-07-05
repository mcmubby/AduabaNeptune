using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Data.Entities
{
    public class ShippingAddress
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ContactPersonsName { get; set; }

        [Required]
        public string Address { get; set; }

        public string City { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string AlternatePhoneNumber { get; set; }

        public string Landmark { get; set; }

        public virtual Customer Customer { get; set; }

        [Required]
        public int CustomerId { get; set; }


        public virtual IEnumerable<Order> Orders { get; set; }
    }
}