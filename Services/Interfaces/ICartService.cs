using System.Collections.Generic;
using System.Threading.Tasks;
using AduabaNeptune.Data.Entities;
using AduabaNeptune.Dto;

namespace AduabaNeptune.Services
{
    public interface ICartService
    {
        //Add item to cart
        //Remove item from cart
        //Edit cartitem quantity
        //Get all items in cart

        Task<CartItemResponse> AddItemToCartAsync(int productId, int customerId);
        Task RemoveItemFromCartAsync(List<int> cartItemId);
        Task<CartItemResponse> EditItemQuantityAsync(EditCartItemRequest cartItemEdit);
        Task<List<CartItemResponse>> GetAllCartItemsAsync(int customerId);
    }
}