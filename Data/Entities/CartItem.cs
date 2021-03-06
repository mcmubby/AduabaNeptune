using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Data.Entities
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        public virtual Product Product { get; set; }

        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public virtual Cart Cart { get; set; }

        public int CartId { get; set; }

        public virtual Order Order { get; set; }

        public string OrderId { get; set; }

        public string CartItemStatus { get; set; }
    }
}