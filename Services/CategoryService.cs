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

        public bool AddCategory(AddCategoryRequest addCategoryRequest)
        {
            var existingCategory = _context.Categories.FirstOrDefault(c => c.CategoryName == addCategoryRequest.CategoryName);

            if(existingCategory != null){return false;}

            var newCategory = new Category
            {
                Id = Guid.NewGuid().ToString(),
                CategoryName = addCategoryRequest.CategoryName
            };

            _context.Categories.Add(newCategory);
            _context.SaveChanges();
            return true;
        }


        public void DeleteCategory(List<string> categoryIds)
        {
            List<Category> categoriesToDelete = new List<Category>();

            categoriesToDelete = _context.Categories.Where(c => categoryIds.Contains(c.Id)).ToList();

            if (categoriesToDelete.Count != 0)
            {
                _context.Categories.RemoveRange(categoriesToDelete);
                _context.SaveChanges();
            }
        }


        public bool EditCategory(EditCategoryRequest editCategoryRequest)
        {
            var oldCategory = _context.Categories.FirstOrDefault(c =>  c.Id == editCategoryRequest.Id);

            if(oldCategory == null){return false;} //Category not found

            oldCategory.CategoryName = editCategoryRequest.NewCategoryName;
            _context.SaveChanges();
            return true;
        }

        public List<Category> GetAllCategories()
        {
            var response = _context.Categories.Include(c => c.SubCategories).ToList();
        
            return response;
        }
    }
}