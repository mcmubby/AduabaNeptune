using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AduabaNeptune.Data;
using AduabaNeptune.Data.Entities;
using AduabaNeptune.Dto;
using Microsoft.EntityFrameworkCore;

namespace AduabaNeptune.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> AddCategoryAsync(AddCategoryRequest addCategoryRequest)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == addCategoryRequest.CategoryName);

            if(existingCategory != null){return null;}

            var newCategory = new Category
            {
                CategoryName = addCategoryRequest.CategoryName
            };

            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();
            return newCategory;
        }


        public async Task DeleteCategoryAsync(List<int> categoryIds)
        {
            List<Category> categoriesToDelete = new List<Category>();

            categoriesToDelete = await _context.Categories.Where(c => categoryIds.Contains(c.Id)).ToListAsync();

            if (categoriesToDelete.Count != 0)
            {
                _context.Categories.RemoveRange(categoriesToDelete);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<bool> EditCategoryAsync(EditCategoryRequest editCategoryRequest)
        {
            var oldCategory = await GetCategoryByIdAsync(editCategoryRequest.Id);

            if(oldCategory == null){return false;} //Category not found

            oldCategory.CategoryName = editCategoryRequest.NewCategoryName;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            var response = await _context.Categories.Include(s => s.SubCategories).ToListAsync();
        
            return response;
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.Categories.Where(c => c.Id == categoryId).FirstOrDefaultAsync();
        }
    }
}