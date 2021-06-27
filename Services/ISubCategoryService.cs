using System.Collections.Generic;
using System.Threading.Tasks;
using AduabaNeptune.Data.Entities;
using AduabaNeptune.Dto;

namespace AduabaNeptune.Services
{
    public interface ISubCategoryService
    {
        Task<List<SubCategory>> GetAllSubCategoriesAsync();
        Task<SubCategory> GetAllSubCategoryByIdAsync(int id);
        Task<SubCategory> AddSubCategoryAsync(AddSubCategoryRequest addSubCategoryModel);
        Task DeleteSubCategoryAsync(List<int> subCategoryIds);
        Task<bool> EditSubCategoryAsync(EditSubCategoryRequest editSubCategoryModel);
    }
}