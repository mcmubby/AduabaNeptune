using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Data.Entities
{
    public class CartItem
    {
        [Key]
        public string Id { get; set; }

        public virtual Product Product { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public virtual Cart Cart { get; set; }

        [Required]
        public int CartId { get; set; }

        public virtual Order Order { get; set; }

        public int OrderId { get; set; }
    }
}