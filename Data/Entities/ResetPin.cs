using System;
using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Data.Entities
{
    public class ResetPin
    {
        [Key]
        public int Id { get; set; }
        public int Pin { get; set; }
        public virtual Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}