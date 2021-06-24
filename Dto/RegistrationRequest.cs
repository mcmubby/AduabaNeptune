using System;
using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Dto
{
    public class RegistrationRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}