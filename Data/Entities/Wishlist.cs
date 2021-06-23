using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Data.Entities
{
    public class WishList
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Customer Customer { get; set; }

        [Required]
        public int CustomerId { get; set; }


        public virtual IEnumerable<WishListItem> WishListItems { get; set; }
    }
}