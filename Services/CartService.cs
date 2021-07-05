using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AduabaNeptune.Data;
using AduabaNeptune.Data.Entities;
using AduabaNeptune.Dto;
using AduabaNeptune.Helper;
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

        public async Task<CartItemResponse> AddItemToCartAsync(int productId, int customerId)
        {
            //Check product validity
            var productExist = await _context.Products.Where(p => p.Id == productId)
                                                      .FirstOrDefaultAsync();

            if(productExist is null || productExist.Quantity == 0){return null;}

            //Check if user has existing cart if not create one
            var existingCart = await _context.Carts.Where(c => c.Customer.Id == customerId)
                                                   .Include(cI => cI.CartItems)
                                                   .FirstOrDefaultAsync();

            if(existingCart is null)
            {
                var cart = new Cart();
                cart.CustomerId = customerId;
                _context.Carts.Add(cart);

                var cartItem = new CartItem();
                cartItem.CartId = cart.Id; //Id might have not been assigned //error prone
                cartItem.Quantity = 1;
                cartItem.ProductId = productId;
                cartItem.CartItemStatus = CartItemStatus.InCart.ToString();

                _context.CartItems.Add(cartItem);

                await _context.SaveChangesAsync();
                return cartItem.AsCartResponseDto();
            }

            //Check if the product already exist in cart.
            //if it exist increase the quantity by 1
            CartItem sameItemAlreadyInCart = null;
            if(existingCart.CartItems != null)
            {
                sameItemAlreadyInCart = existingCart.CartItems.Where(p => p.ProductId == productId && p.CartItemStatus == CartItemStatus.InCart.ToString())
                                                             .FirstOrDefault();
            }

            if(sameItemAlreadyInCart is null)
            {
                var cartItem = new CartItem();
                cartItem.CartId =existingCart.Id;
                cartItem.Quantity = 1;
                cartItem.ProductId = productId;
                cartItem.CartItemStatus = CartItemStatus.InCart.ToString();

                _context.CartItems.Add(cartItem);
                await _context.SaveChangesAsync();
                return cartItem.AsCartResponseDto();
            }

            sameItemAlreadyInCart.Quantity += 1;
            await _context.SaveChangesAsync();
            return sameItemAlreadyInCart.AsCartResponseDto();
        }

        public async Task<CartItemResponse> EditItemQuantityAsync(EditCartItemRequest cartItemEdit)
        {
            //Find the cart item and change its quantity
            var cartItem = await _context.CartItems.Where(c => c.Id == cartItemEdit.CartItemId)
                .Include(p => p.Product)
                .FirstOrDefaultAsync();

            if(cartItem is null){return null;}

            var quantityAvailableAfterEdit = cartItem.Product.Quantity - cartItemEdit.Quantity;

            if(quantityAvailableAfterEdit < 0){return null;}

            cartItem.Quantity = cartItemEdit.Quantity;

            await _context.SaveChangesAsync();
            return cartItem.AsCartResponseDto();
        }

        public async Task<List<CartItemResponse>> GetAllCartItemsAsync(int customerId)
        {
            var cart =  await _context.Carts.Where(c => c.Customer.Id == customerId)
                .Include(p => p.CartItems)
                .ThenInclude(i => i.Product)
                .Select(cIs => cIs.CartItems)
                .FirstOrDefaultAsync();

            var response = new List<CartItemResponse>();

            if(cart is null){return null;}

            foreach (var item in cart)
            {
                if(item.CartItemStatus == CartItemStatus.InCart.ToString())
                {
                    response.Add(item.AsCartResponseDto());
                }
            }
            return response;
        }

        public async Task RemoveItemFromCartAsync(int cartItemId)
        {
            var item = await _context.CartItems.Where(i => i.Id == cartItemId)
                                               .FirstOrDefaultAsync();

            if(item != null)
            {
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}