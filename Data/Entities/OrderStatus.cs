using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Data.Entities
{
    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Status { get; set; }
    }
}