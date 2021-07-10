using System.Collections.Generic;
using System.Threading.Tasks;
using AduabaNeptune.Dto;
using AduabaNeptune.Helper;
using AduabaNeptune.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AduabaNeptune.Controllers
{
    [Authorize]
    [ApiController]
    [Route("shipping-details")]
    public class ShippingAddressController : ControllerBase
    {
        private readonly IShippingAddressService _shippingService;

        public ShippingAddressController(IShippingAddressService shippingService)
        {
            _shippingService = shippingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShippingAddresses()
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User);

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            var response = await _shippingService.GetAllShippingAddressesAsync(requesterIdentity);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddShippingAddress([FromBody]AddShippingAddressRequest request)
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User);

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            var response = await _shippingService.AddShippingAddressAsync(request, requesterIdentity);

            return CreatedAtAction(nameof(GetShippingAddressById), new{id = response.ShippingAddressId}, response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetShippingAddressById(int id)
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User);

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            var response = await _shippingService.GetShippingAddressById(id, requesterIdentity);
            if(response == null){return NotFound();}
            return Ok(response);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteShippingAddresses([FromBody] List<int> shippingAddressIds)
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User);

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            await _shippingService.DeleteShippingAddress(shippingAddressIds, requesterIdentity);

            return NoContent();
        }
    }
}