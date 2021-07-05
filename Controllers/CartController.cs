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
        public async Task<IActionResult> AddItemToCart([FromBody]AddCartItemRequest addCartItem)
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User);

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            var response = await _cartService.AddItemToCartAsync(addCartItem.ProductId, requesterIdentity);

            if(response is null){return BadRequest(new {message = "Product could not be added to cart"});}

            return Ok(response);
        }


        [HttpPatch]
        public async Task<IActionResult> EditItemQuantity([FromBody]EditCartItemRequest editCartItem)
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User);

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            var response = await _cartService.EditItemQuantityAsync(editCartItem);

            if(response is null){return BadRequest(new {message = "Product quantity could not be changed"});}

            return Ok(response);
        }


        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> DeleteItemFromCart(int cartItemId)
        {
            var requesterIdentity = ClaimsProcessor.CheckClaimForCustomerId(HttpContext.User);

            if (requesterIdentity == 0)
            {
                return Unauthorized();
            }

            await _cartService.RemoveItemFromCartAsync(cartItemId);

            return NoContent();
        }
    }
}