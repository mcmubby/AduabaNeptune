using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Dto
{
    public class PasswordResetPinRequest
    {
        [Required]
        public bool SendToEmail { get; set; }
        [Required]
        public bool SendToSms { get; set; }
        [Required]
        public string CustomerEmail { get; set; }

    }
}