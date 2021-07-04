using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Dto
{
    public class AddCartItemRequest
    {
        [Required]
        public int ProductId { get; set; }
    }
}