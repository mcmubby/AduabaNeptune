using System.Collections.Generic;
using AduabaNeptune.Data.Entities;
using AduabaNeptune.Dto;

namespace AduabaNeptune.Services
{
    public interface ICategoryService
    {
        List<Category> GetAllCategories();
        bool AddCategory(AddCategoryRequest addCategoryRequest);
        void DeleteCategory(List<string> categoryIds);
        bool EditCategory(EditCategoryRequest editCategoryRequest);
    }
}