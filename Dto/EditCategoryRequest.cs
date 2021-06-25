using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Dto
{
    public class EditCategoryRequest
    {
        [Required]
        public string Id { get; set; }
        public string NewCategoryName { get; set; }
    }
}