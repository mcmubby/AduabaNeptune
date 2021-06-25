using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Dto
{
    public class UpdateCustomerRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public string AvatarUrl { get; set; }
    }
}