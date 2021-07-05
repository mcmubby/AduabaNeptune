using System.Collections.Generic;
using System.Threading.Tasks;
using AduabaNeptune.Dto;
using AduabaNeptune.Helper;

namespace AduabaNeptune.Services
{
    public interface IProductService
    {
        Task<List<GetProductResponse>> GetAllProductsAsync(Filter filter);
        Task<GetProductResponse> GetProductByIdAsync(int productId);
        Task<List<GetProductResponse>> GetProductsByCategoryAsync(int categoryId, Filter filter);
        Task<List<GetProductResponse>> GetProductsBySearchKeyAsync(string searchKeyWord, Filter filter);
        Task<bool> EditProductQuantityAsync(EditProductQuantityRequest editProductQuantity);
        Task<int> GetTotalRecords();
    }
}