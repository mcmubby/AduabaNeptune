using System.Collections.Generic;
using AduabaNeptune.Data.Entities;
using AduabaNeptune.Dto;

namespace AduabaNeptune.Services
{
    public interface ISubCategoryService
    {
        List<SubCategory> GetAllSubCategories();
        bool AddSubCategory(AddSubCategoryRequest addSubCategoryModel);
        void DeleteSubCategory(List<int> subCategoryIds);
        bool EditSubCategory(EditSubCategoryRequest editSubCategoryModel);
    }
}