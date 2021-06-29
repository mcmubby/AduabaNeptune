using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Dto
{
    public class EditProductQuantityRequest
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}