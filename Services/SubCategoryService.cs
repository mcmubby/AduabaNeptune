using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<SubCategory> AddSubCategoryAsync(AddSubCategoryRequest addSubCategoryModel)
        {
            var existingSubCategory = await _context.SubCategories.FirstOrDefaultAsync(c => c.Name == addSubCategoryModel.SubCategoryName);

            if(existingSubCategory != null){return null;}

            var newSubCategory = new SubCategory
            {
                Name = addSubCategoryModel.SubCategoryName,
                CategoryId = addSubCategoryModel.CategoryId
            };

            await _context.SubCategories.AddAsync(newSubCategory);
            await _context.SaveChangesAsync();
            return newSubCategory;
        }

        public async Task DeleteSubCategoryAsync(List<int> subCategoryIds)
        {
            List<SubCategory> subCategoriesToDelete = new List<SubCategory>();

            subCategoriesToDelete = await _context.SubCategories.Where(c => subCategoryIds.Contains(c.Id)).ToListAsync();

            if (subCategoriesToDelete.Count != 0)
            {
                _context.SubCategories.RemoveRange(subCategoriesToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> EditSubCategoryAsync(EditSubCategoryRequest editSubCategoryModel)
        {
            var oldSubCategory = await _context.SubCategories.FirstOrDefaultAsync(c =>  c.Id == editSubCategoryModel.Id);

            if(oldSubCategory == null){return false;} //SubCategory not found

            var categoryExist = await _context.Categories.FirstOrDefaultAsync(c => c.Id == editSubCategoryModel.CategoryId); //checks if a category exist with the provided category id

            if(categoryExist == null){return false;} //new Category not found

            oldSubCategory.Name = editSubCategoryModel.NewSubCategoryName;
            oldSubCategory.CategoryId =editSubCategoryModel.CategoryId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<SubCategory>> GetAllSubCategoriesAsync()
        {
            var response = await _context.SubCategories.ToListAsync();
            return response;
        }

        public async Task<SubCategory> GetAllSubCategoryByIdAsync(int subCategoryId)
        {
            return await _context.SubCategories.FirstOrDefaultAsync(s => s.Id == subCategoryId);
        }
    }
}