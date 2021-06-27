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
    [Route("Subcategory")]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        [HttpGet]
        public IActionResult GetAllSubCategories()
        {

            List<SubCategory> subCategories = new List<SubCategory>();
            List<GetSubCategoryResponse> subCategoriesResponse = new List<GetSubCategoryResponse>();

            subCategories = _subCategoryService.GetAllSubCategories();

            foreach (var subCategory in subCategories)
            {
                subCategoriesResponse.Add(new GetSubCategoryResponse{
                    CategoryId = subCategory.CategoryId,
                    SubCategoryName = subCategory.Name,
                    Id = subCategory.Id
                });
            }

            return Ok(subCategoriesResponse);
        }


        [Authorize]
        [HttpPost]
        public IActionResult AddCategory([FromBody]AddSubCategoryRequest model)
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User);//Change to admin //Only using this to test

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            var response = _subCategoryService.AddSubCategory(model);

            return Ok(new {message = "Sub-category added"});
        }


        [Authorize]
        [HttpDelete]
        public IActionResult DeleteCategory([FromBody]List<int> subCategoryIds)
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User);//Change to admin //Only using this to test

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            if(subCategoryIds.Count == 0){return BadRequest(new {message = "No sub-category Id in request"});}

            _subCategoryService.DeleteSubCategory(subCategoryIds);
            return Ok();
        }


        [Authorize]
        [HttpPut]
        public IActionResult EditCategory([FromBody]EditSubCategoryRequest model)
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User); //Change to admin //Only using this to test

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            var response =_subCategoryService.EditSubCategory(model);

            if(!response){return BadRequest(new {message = "Sub-category to be edited not found"});}

            return Ok();
        }
    }
}