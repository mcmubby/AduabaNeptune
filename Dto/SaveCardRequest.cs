using System;
using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Dto
{
    public class SaveCardRequest
    {
        [Required]
        public string CardHolderName { get; set; }

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public string CCV { get; set; }
    }
}