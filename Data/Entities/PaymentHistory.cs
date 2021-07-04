using System;
using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Data.Entities
{
    public class PaymentHistory
    {
        [Key]
        public int Id { get; set; }
        
        public virtual Order Order { get; set; }

        [Required]
        public string OrderNumber { get; set; }

        public virtual PaymentStatus PaymentStatus { get; set; }

        [Required]
        public int PaymentStatusId { get; set; }

        [Required]
        public DateTime LastModified { get; set; }
    }
}