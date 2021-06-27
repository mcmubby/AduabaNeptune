using System.Collections.Generic;
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
        public IActionResult GetAllCategories()
        {

            List<Category> categories = new List<Category>();
            categories = _categoryService.GetAllCategories();

            return Ok(categories);
        }


        [Authorize]
        [HttpPost]
        public IActionResult AddCategory([FromBody]AddCategoryRequest model)
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User);//Change to admin //Only using this to test

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            var response = _categoryService.AddCategory(model);

            return Ok();
        }


        [Authorize]
        [HttpDelete]
        public IActionResult DeleteCategory([FromBody]List<string> categoryIds)
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User);//Change to admin //Only using this to test

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            if(categoryIds.Count == 0){return BadRequest(new {message = "No category Id in request"});}

            _categoryService.DeleteCategory(categoryIds);
            return Ok();
        }


        [Authorize]
        [HttpPut]
        public IActionResult EditCategory([FromBody]EditCategoryRequest model)
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User); //Change to admin //Only using this to test

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            var response =_categoryService.EditCategory(model);

            if(!response){return BadRequest(new {message = "Category to be edited not found"});}

            return Ok();
        }
    }
}