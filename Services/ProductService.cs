using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AduabaNeptune.Data;
using AduabaNeptune.Data.Entities;
using AduabaNeptune.Dto;
using AduabaNeptune.Helper;
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

        public async Task<List<GetProductResponse>> GetAllProductsAsync(Filter filter)
        {
            var response = new List<GetProductResponse>();
            var products = await _context.Products.Where(p => p.Quantity != 0).Include(v => v.Vendor)
                                                  .Include(c => c.Category)
                                                  .OrderBy(p => p.DateAdded)
                                                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                                                  .Take(filter.PageSize)
                                                      .ToListAsync();
            foreach (var product in products)
            {
                response.Add(product.AsProductResponseDto());                
            }
            return response;
        }

        public async Task<GetProductResponse> GetProductByIdAsync(string productId)
        {
            var product = await _context.Products.Where(p => p.Id == productId
                                                             && p.Quantity != 0).Include(v => v.Vendor)
                                                 .Include(c => c.Category)
                                                 .FirstOrDefaultAsync();
            return product.AsProductResponseDto();
        }

        public async Task<IEnumerable<GetProductResponse>> GetProductsByCategoryAsync(string categoryId, Filter filter)//There might be an error here the cat Id selector in the linq
        {
            var response = new List<GetProductResponse>();
            var products = await _context.Products.Where(p => p.Category.Id == categoryId
                                                              && p.Quantity != 0).Include(v => v.Vendor)
                                                  .Include(c => c.Category)
                                                  .OrderBy(p => p.DateAdded)
                                                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                                                  .Take(filter.PageSize)
                                                  .ToListAsync();
            foreach (var product in products)
            {
                response.Add(product.AsProductResponseDto());                
            }
            return response;
        }

        public async Task<IEnumerable<GetProductResponse>> GetProductsBySearchKeyAsync(string searchKeyWord, Filter filter)
        {
            var response = new List<GetProductResponse>();
            var products = await _context.Products.Where(p => p.Description.Contains(searchKeyWord)
                                                              || p.Name.Contains(searchKeyWord)
                                                              && p.Quantity != 0).Include(v => v.Vendor)
                                                  .Include(c => c.Category)
                                                  .OrderBy(p => p.DateAdded)
                                                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                                                  .Take(filter.PageSize)
                                                  .ToListAsync();
            foreach (var product in products)
            {
                response.Add(product.AsProductResponseDto());                
            }
            return response;
        }

        public async Task<int> GetTotalRecords()
        {
            return await _context.Products.CountAsync();
        }
    }
}