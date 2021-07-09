using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Dto
{
    public class PinVerificationRequest
    {
        [Required]
        public int Pin { get; set; }
        [Required]
        public string Email { get; set; }
    }
}