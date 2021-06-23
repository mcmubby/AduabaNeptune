using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Data.Entities
{
    public class PaymentStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Status { get; set; }
    }
}