using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Dto
{
    public class UpdateCustomerRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        public string Base64ImageString { get; set; }
    }
}