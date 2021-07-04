using System;
using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Data.Entities
{
    public class Card
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CardHolderName { get; set; }

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public string CVV { get; set; }

        public virtual Customer Customer { get; set; }

        [Required]
        public int CustomerId { get; set; }
    }
}