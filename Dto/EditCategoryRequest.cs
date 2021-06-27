using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Dto
{
    public class EditCategoryRequest
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string NewCategoryName { get; set; }
    }
}