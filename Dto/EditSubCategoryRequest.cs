using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Dto
{
    public class EditSubCategoryRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string NewSubCategoryName { get; set; }
        [Required]
        public string CategoryId { get; set; }
    }
}