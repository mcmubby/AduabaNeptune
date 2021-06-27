using System.Collections.Generic;
using System.Threading.Tasks;
using AduabaNeptune.Data.Entities;
using AduabaNeptune.Dto;

namespace AduabaNeptune.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(string categoryId);
        Task<Category> AddCategoryAsync(AddCategoryRequest addCategoryRequest);
        Task DeleteCategoryAsync(List<string> categoryIds);
        Task<bool> EditCategoryAsync(EditCategoryRequest editCategoryRequest);
    }
}