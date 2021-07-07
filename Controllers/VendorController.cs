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
    [Route("Vendor")]
    public class VendorController : ControllerBase
    {
        private readonly IVendorService _vendorService;

        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        [HttpGet]
        public async Task<IActionResult<IEnumerable<Vendor>>> GetVendors()
        {
            try
            {
                return Ok(await _vendorService.GetVendors()).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
        [HttpGet("{id: int}")]
        public async Task<IActionResult<Vendor>> GetVendorById(int id)
        {
            try
            {
                var result = await _vendorService.GetVendorById(id);
                if (result == null) return NotFound();
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }     
        } 
        [HttpPost]
        public async Task<IActionResult<Vendor>> CreateVendor([FromBody]Vendor vendor)
        {
            try
            {
                if (vendor == null)
                    return BadRequest();
                var createdVendor = await _vendorService.AddVendor(vendor);
                return CreatedAtAction(nameof(GetVendor)), new {id = createdVendor.VendorId}, createdVendor);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new employee record");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVendor(int id, [FromBody] Vendor vendor)
        {
            try
            {
                if (id != vendor.VendorId)
                    return BadRequest("Vendor ID mismatch");
                var vendorToUpdate = await _vendorService.GetVendor(id);
                if (vendorToUpdate == null)
                    return NotFound($"Vendor with Id = {id} not found");
                return await _vendorService.UpdateVendor(vendor);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult<Vendor>> DeleteVendor(int id)
        {
            try
            {
                var vendorToDelete = await _vendorService.GetVendor(id);
                if(vendorToDelete == null)
                {
                    return NotFound($"Vendor with Id = {id} not found");
                }

                return await _vendorService.DeleteVendor(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }
    }
}