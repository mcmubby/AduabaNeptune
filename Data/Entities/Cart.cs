using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Data.Entities
{
    public class Cart
    {
        [Key]
        public string Id { get; set; }

        public virtual Customer Customer { get; set; }

        public int CustomerId { get; set; }

        public virtual List<CartItem> CartItems { get; set; }
    }
}