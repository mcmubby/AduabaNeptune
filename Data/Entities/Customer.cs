using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Data.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public virtual ShippingAddress ShippingAddress { get; set; }
        public int ShippingAddressId { get; set; }
        public virtual BillingAddress BillingAddress { get; set; }
        public int BillingAddressId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }

        [Required]
        public string Password { get; set; }
        public string AvatarUrl { get; set; }



        public virtual IEnumerable<BillingAddress> BillingAddresses { get; set; }
        public virtual IEnumerable<ShippingAddress> ShippingAddresses { get; set; }
        public virtual IEnumerable<Card> Cards { get; set; }
        public virtual IEnumerable<Cart> Cart { get; set; }
        public virtual IEnumerable<CartItem> CartItems { get; set; }
        public virtual IEnumerable<WishList> Wishlist { get; set; }
        public virtual IEnumerable<Order> Orders { get; set; }
        public virtual IEnumerable<PaymentHistory> PaymentHistories { get; set; }
    }
}