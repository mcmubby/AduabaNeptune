using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Data.Entities
{
    public class Vendor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ShopName { get; set; }

        public string VendorName { get; set; }

        [Required]
        public DateTime DateJoined { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; } = "Vendor";


        public virtual IEnumerable<Product> Products { get; set; }

    }
}