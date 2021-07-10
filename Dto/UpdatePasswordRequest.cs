using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Dto
{
    public class UpdatePasswordRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}