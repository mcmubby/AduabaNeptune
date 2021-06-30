using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Data.Entities
{
    public class Cart
    {
        [Key]
        public string Id { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual IEnumerable<CartItem> CartItems { get; set; }
    }
}