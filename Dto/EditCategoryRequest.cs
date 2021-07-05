using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Dto
{
    public class EditCategoryRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string NewCategoryName { get; set; }
    }
}