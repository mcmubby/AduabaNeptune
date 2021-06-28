using System.Collections.Generic;
using System.Threading.Tasks;
using AduabaNeptune.Data;
using AduabaNeptune.Data.Entities;

namespace AduabaNeptune.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Product> GetProductByIdAsync(string productId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductsBySearchKeyAsync(string searchKeyWord)
        {
            throw new System.NotImplementedException();
        }
    }
}