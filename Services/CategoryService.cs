using System.Collections.Generic;
using System.Linq;
using AduabaNeptune.Data;
using AduabaNeptune.Data.Entities;
using AduabaNeptune.Dto;

namespace AduabaNeptune.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddCategory(AddCategoryRequest addCategoryRequest)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCategory(List<string> categoryIds)
        {
            throw new System.NotImplementedException();
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
            var response = _context.Categories.ToList();
            
            return response;
        }
    }
}