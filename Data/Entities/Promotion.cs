using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Data.Entities
{
    public class Promotion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PromotionTitle { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}