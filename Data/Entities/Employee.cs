using System;
using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Data.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string OfficialEmail { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime LastModified { get; set; }

        [Required]
        public string Password { get; set; }

        public string AvatarUrl { get; set; }

        public string Role { get; set; }
    }
}