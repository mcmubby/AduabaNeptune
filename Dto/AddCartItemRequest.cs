using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Dto
{
    public class AddCartItemRequest
    {
        [Required]
        public string ProductId { get; set; }
    }
}