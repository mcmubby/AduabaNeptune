using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Data.Entities
{
    public class WishListItem
    {
        [Key]
        public int Id { get; set; }

        public virtual Product Product { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual WishList WishList { get; set; }

        [Required]
        public int WishListId { get; set; }
    }
}