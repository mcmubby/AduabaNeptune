using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Data.Entities
{
    public class Product
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        public virtual Category Category { get; set; }

        [Required]
        public string CategoryId { get; set; }

        public string SubCategory { get; set; }

        public virtual Vendor Vendor { get; set; }

        [Required]
        public int VendorId { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        public string ImageUrl { get; set; }


        // public virtual IEnumerable<CartItem> CartItems { get; set; }
        // public virtual IEnumerable<WishListItem> WishListItems { get; set; }
    }
}