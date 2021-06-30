using System.Threading.Tasks;
using AduabaNeptune.Helper;
using AduabaNeptune.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AduabaNeptune.Controllers
{
    [Authorize]
    [ApiController]
    [Route("cart")]   
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCartItems()
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User);

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            var response = await _cartService.GetAllCartItemsAsync(requesterIdentity);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToCart([FromBody]string productId)
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User);

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            var response = await _cartService.GetAllCartItemsAsync(requesterIdentity);
            return Ok(response);
        }
    }
}