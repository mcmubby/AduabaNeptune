using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Dto
{
    public class EditProductQuantityRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}