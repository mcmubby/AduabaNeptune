using System.Threading.Tasks;
using AduabaNeptune.Helper;
using AduabaNeptune.Services;
using Microsoft.AspNetCore.Mvc;

namespace AduabaNeptune.Controllers
{
    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private readonly IUriService _uriService;
        private readonly IProductService _productService;

        public ProductController(IProductService productService, IUriService uriService)
        {
            _uriService = uriService;
            _productService = productService;
        }

//Paginated endpoint
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] Filter filter)
        {
            var route = Request.Path.Value;

            var validatedFilter = new Filter(filter.PageNumber, filter.PageSize);

            var products = await _productService.GetAllProductsAsync(validatedFilter);//validatedFilter

            var totalRecords = await _productService.GetTotalRecords();

            var response = products.CreatePagedReponse(validatedFilter, totalRecords, _uriService, route);

            return Ok(response);
        }


        // [HttpGet]
        // public async Task<IActionResult> GetAllProducts()
        // {

        //     var products = await _productService.GetAllProductsAsync();

        //     return Ok(products);
        // }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProductById(int Id)
        {
            var product = await _productService.GetProductByIdAsync(Id);

            return Ok(product);
        }


        [HttpGet]
        [Route("category")]
        public async Task<IActionResult> GetProductsByCategory([FromQuery] Filter filter, [FromQuery]int categoryId)
        {
            var route = Request.Path.Value;

            var validatedFilter = new Filter(filter.PageNumber, filter.PageSize);

            var products = await _productService.GetProductsByCategoryAsync(categoryId, validatedFilter);

            var totalRecords = await _productService.GetTotalRecords();

            var response = products.CreatePagedReponse(validatedFilter, totalRecords, _uriService, route);

            return Ok(response);
        }


        // [HttpGet]
        // [Route("category")]
        // public async Task<IActionResult> GetProductsByCategory([FromQuery]int categoryId)
        // {
        //     var products = await _productService.GetProductsByCategoryAsync(categoryId);

        //     return Ok(products);
        // }


        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetProductsBySearch([FromQuery] Filter filter, [FromQuery]string searchKey)
        {
            var route = Request.Path.Value;

            var validatedFilter = new Filter(filter.PageNumber, filter.PageSize);

            var products = await _productService.GetProductsBySearchKeyAsync(searchKey, validatedFilter);

            var totalRecords = await _productService.GetTotalRecords();

            var response = products.CreatePagedReponse(validatedFilter, totalRecords, _uriService, route);

            return Ok(response);
        }


        // [HttpGet]
        // [Route("search")]
        // public async Task<IActionResult> GetProductsBySearch([FromQuery]string searchKey)
        // {
        //     var products = await _productService.GetProductsBySearchKeyAsync(searchKey);

        //     return Ok(products);
        // }
    }
}