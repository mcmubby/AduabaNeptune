using System.Collections.Generic;
using System.Threading.Tasks;
using AduabaNeptune.Data.Entities;
using AduabaNeptune.Dto;
using AduabaNeptune.Helper;
using AduabaNeptune.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AduabaNeptune.Controllers
{
    [ApiController]
    [Route("Category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {

            List<Category> categories = new List<Category>();
            categories = await _categoryService.GetAllCategoriesAsync();

            return Ok(categories);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {

            var category = await _categoryService.GetCategoryByIdAsync(id);
            if(category is null){return NotFound();}

            return Ok(category);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody]AddCategoryRequest model)
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User);//Change to admin //Only using this to test

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            var response = await _categoryService.AddCategoryAsync(model);

            return CreatedAtAction(nameof(GetCategoryById), new{id = response.Id}, response);
        }


        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory([FromBody]List<int> categoryIds)
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User);//Change to admin //Only using this to test

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            if(categoryIds.Count == 0){return BadRequest(new {message = "No category Id in request"});}

            await _categoryService.DeleteCategoryAsync(categoryIds);
            return NoContent();
        }


        [Authorize]
        [HttpPut]
        public async Task<IActionResult> EditCategory([FromBody]EditCategoryRequest model)
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User); //Change to admin //Only using this to test

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            var response = await _categoryService.EditCategoryAsync(model);

            if(!response){return BadRequest(new {message = "Category to be edited not found"});}

            return NoContent();

        }
    }
}