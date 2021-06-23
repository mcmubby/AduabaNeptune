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

        [Required]
        public DateTime DateJoined { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }


        public virtual IEnumerable<Product> Products { get; set; }

    }
}