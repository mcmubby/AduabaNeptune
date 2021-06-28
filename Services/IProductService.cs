using System.Collections.Generic;
using System.Threading.Tasks;
using AduabaNeptune.Data.Entities;
using AduabaNeptune.Dto;
using AduabaNeptune.Helper.Pagination;

namespace AduabaNeptune.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync(Filter filter);
        Task<Product> GetProductByIdAsync(string productId);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryId, Filter filter);
        Task<IEnumerable<Product>> GetProductsBySearchKeyAsync(string searchKeyWord, Filter filter);
        Task<bool> EditProductQuantityAsync(EditProductQuantityRequest editProductQuantity);
        Task<int> GetTotalRecords();
    }
}