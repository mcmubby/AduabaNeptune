using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Data.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CategoryName { get; set; }
    }
}