using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AduabaNeptune.Data;
using AduabaNeptune.Data.Entities;
using AduabaNeptune.Dto;
using Microsoft.EntityFrameworkCore;

namespace AduabaNeptune.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddItemToCartAsync(string productId, int customerId)
        {
            //Check if user has existing cart if not creat one
            var existingCart = await _context.Carts.Where(c => c.Customer.Id == customerId).FirstOrDefaultAsync();

            if(existingCart is null)
            {
                var cart = new Cart();
                cart.Customer.Id = customerId;
                cart.Id = Guid.NewGuid().ToString();

                var cartItem = new CartItem();
                cartItem.Id = Guid.NewGuid().ToString();
                cartItem.Cart.Id =cart.Id;
                cartItem.Quantity = 1;
                cartItem.Product.Id = productId;
                cartItem.CartItemStatus = CartItemStatus.InCart.ToString();

                await _context.CartItems.AddAsync(cartItem);
            }

            //Check if the product already exist in cart.
            //if it exist increase the quantity by 1
            var existingCartItem = await _context.CartItems.Where(p => p.Product.Id == productId)
                                                           .FirstOrDefaultAsync();

            if(existingCartItem is null)
            {
                var cartItem = new CartItem();
                cartItem.Id = Guid.NewGuid().ToString();
                cartItem.Cart.Id =existingCart.Id;
                cartItem.Quantity = 1;
                cartItem.Product.Id = productId;
                cartItem.CartItemStatus = CartItemStatus.InCart.ToString();

                await _context.CartItems.AddAsync(cartItem);
            }

            existingCartItem.Quantity += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<CartItem> EditItemQuantityAsync(EditCartItemRequest cartItemEdit)
        {
            //Find the cart item and change its quantity
            var cartItem = await _context.CartItems.Where(c => c.Id == cartItemEdit.CartItemId).FirstOrDefaultAsync();

            if(cartItem is null){return null;}

            var quantityAvailableAfterEdit = cartItem.Product.Quantity - cartItemEdit.Quantity;

            if(quantityAvailableAfterEdit < 1){return null;}

            cartItem.Quantity = cartItemEdit.Quantity;

            await _context.SaveChangesAsync();
            return cartItem;
        }

        public async Task<List<CartItem>> GetAllCartItemsAsync(int customerId)
        {
            var cart =  await _context.Carts.Where(c => c.Customer.Id == customerId)
                                            .Include(cI => cI.CartItems).Select(cIs => cIs.CartItems)
                                            .ToListAsync();

            var response = new List<CartItem>();

            var enumerableOfCartItems = cart[0];

            if(enumerableOfCartItems is null){return null;}

            foreach (var item in enumerableOfCartItems)
            {
                if(item.CartItemStatus == CartItemStatus.InCart.ToString())
                {
                    response.Add(item);
                }
            }
            return response;
        }

        public async Task RemoveItemFromCartAsync(string cartItemId)
        {
            var item = await _context.CartItems.Where(i => i.Id == cartItemId)
                                               .FirstOrDefaultAsync();

            if(item != null){
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}