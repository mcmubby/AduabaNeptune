using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Dto
{
    public class SignInRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}