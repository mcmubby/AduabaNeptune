using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AduabaNeptune.Data;
using AduabaNeptune.Data.Entities;
using AduabaNeptune.Dto;
using AduabaNeptune.Helper.Pagination;
using Microsoft.EntityFrameworkCore;

namespace AduabaNeptune.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> EditProductQuantityAsync(EditProductQuantityRequest editProductQuantity)
        {
            var product = await GetProductByIdAsync(editProductQuantity.Id);

            if(product is null){return false;}

            product.Quantity = editProductQuantity.Quantity;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Product>> GetAllProductsAsync(Filter filter)
        {
            var pagedProduct = await _context.Products.Where(p => p.Quantity != 0)
                                                      .Skip((filter.PageNumber - 1) * filter.PageSize)
                                                      .Take(filter.PageSize)
                                                      .ToListAsync();

            return pagedProduct;
        }

        public async Task<Product> GetProductByIdAsync(string productId)
        {
            var product = await _context.Products.Where(p => p.Id == productId
                                                             && p.Quantity != 0)
                                                 .FirstOrDefaultAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryId, Filter filter)
        {
            var products = await _context.Products.Where(p => p.CategoryId == categoryId
                                                              && p.Quantity != 0)
                                                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                                                  .Take(filter.PageSize)
                                                  .ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetProductsBySearchKeyAsync(string searchKeyWord, Filter filter)
        {
            var products = await _context.Products.Where(p => p.Description.Contains(searchKeyWord)
                                                              || p.Name.Contains(searchKeyWord)
                                                              && p.Quantity != 0)
                                                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                                                  .Take(filter.PageSize)
                                                  .ToListAsync();
            return products;
        }

        public async Task<int> GetTotalRecords()
        {
            return await _context.Products.CountAsync();
        }
    }
}