using System.ComponentModel.DataAnnotations;

namespace AduabaNeptune.Dto
{
    public class AddSubCategoryRequest
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string SubCategoryName { get; set; }
    }
}