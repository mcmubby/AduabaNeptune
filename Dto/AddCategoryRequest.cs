using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Dto
{
    public class AddCategoryRequest
    {
        [Required]
        public string CategoryName { get; set; }
    }
}