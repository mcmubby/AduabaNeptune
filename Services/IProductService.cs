using System.Collections.Generic;
using System.Threading.Tasks;
using AduabaNeptune.Data.Entities;

namespace AduabaNeptune.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(string productId);
        Task<IEnumerable<Product>> GetProductsBySearchKeyAsync(string searchKeyWord);
        //get by category
        //get by vendor
    }
}