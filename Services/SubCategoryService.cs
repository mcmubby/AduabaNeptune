using System.Collections.Generic;
using System.Linq;
using AduabaNeptune.Data;
using AduabaNeptune.Data.Entities;
using AduabaNeptune.Dto;
using Microsoft.EntityFrameworkCore;

namespace AduabaNeptune.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ApplicationDbContext _context;

        public SubCategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AddSubCategory(AddSubCategoryRequest addSubCategoryModel)
        {
            var existingSubCategory = _context.SubCategories.FirstOrDefault(c => c.Name == addSubCategoryModel.SubCategoryName);

            if(existingSubCategory != null){return false;}

            var newSubCategory = new SubCategory
            {
                Name = addSubCategoryModel.SubCategoryName,
                CategoryId = addSubCategoryModel.CategoryId
            };

            _context.SubCategories.Add(newSubCategory);
            _context.SaveChanges();
            return true;
        }

        public void DeleteSubCategory(List<int> subCategoryIds)
        {
            List<SubCategory> subCategoriesToDelete = new List<SubCategory>();

            subCategoriesToDelete = _context.SubCategories.Where(c => subCategoryIds.Contains(c.Id)).ToList();

            if (subCategoriesToDelete.Count != 0)
            {
                _context.SubCategories.RemoveRange(subCategoriesToDelete);
                _context.SaveChanges();
            }
        }

        public bool EditSubCategory(EditSubCategoryRequest editSubCategoryModel)
        {
            var oldSubCategory = _context.SubCategories.FirstOrDefault(c =>  c.Id == editSubCategoryModel.Id);

            if(oldSubCategory == null){return false;} //SubCategory not found

            var newCategory = _context.Categories.FirstOrDefault(c => c.Id == editSubCategoryModel.CategoryId);

            if(newCategory == null){return false;} //new Category not found

            oldSubCategory.Name = editSubCategoryModel.NewSubCategoryName;
            oldSubCategory.CategoryId =editSubCategoryModel.CategoryId;
            _context.SaveChanges();
            return true;
        }

        public List<SubCategory> GetAllSubCategories()
        {
            var response = _context.SubCategories.ToList();
            return response;
        }
    }
}