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
    [Route("Subcategory")]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubCategories()
        {

            List<SubCategory> subCategories = new List<SubCategory>();
            List<GetSubCategoryResponse> subCategoriesResponse = new List<GetSubCategoryResponse>();

            subCategories = await _subCategoryService.GetAllSubCategoriesAsync();

            foreach (var subCategory in subCategories)
            {
                subCategoriesResponse.Add(subCategory.AsSubCategoryResponseDto());
            }

            return Ok(subCategoriesResponse);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubCategoryById(int id)
        {

            var subCategory = await _subCategoryService.GetAllSubCategoryByIdAsync(id);

            if(subCategory is null){return NotFound();}

            return Ok(subCategory.AsSubCategoryResponseDto());
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody]AddSubCategoryRequest model)
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User);//Change to admin //Only using this to test

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            var response = await _subCategoryService.AddSubCategoryAsync(model);

            return CreatedAtAction(nameof(GetSubCategoryById), new{id = response.Id}, response.AsSubCategoryResponseDto());
        }


        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory([FromBody]List<int> subCategoryIds)
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User);//Change to admin //Only using this to test

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            if(subCategoryIds.Count == 0){return BadRequest(new {message = "No sub-category Id in request"});}

            await _subCategoryService.DeleteSubCategoryAsync(subCategoryIds);
            return NoContent();
        }


        [Authorize]
        [HttpPut]
        public async Task<IActionResult> EditCategory([FromBody]EditSubCategoryRequest model)
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User); //Change to admin //Only using this to test

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            var response = await _subCategoryService.EditSubCategoryAsync(model);

            if(!response){return BadRequest(new {message = "Sub-category to be edited not found"});}

            return NoContent();
        }
    }
}