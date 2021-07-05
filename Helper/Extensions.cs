using System;
using System.Collections.Generic;
using AduabaNeptune.Data.Entities;
using AduabaNeptune.Dto;
using AduabaNeptune.Services;

namespace AduabaNeptune.Helper
{
    public static class Extensions
    {
        public static GetCustomerCardResponse AsCardResponseDto(this Card card)
        {
            return new GetCustomerCardResponse
            {
                CardId = card.Id,
                CardHolderName = card.CardHolderName,
                CardNumber = card.CardNumber,
                ExpiryDate = card.ExpiryDate,
                CCV = card.CVV
            };
        }

        public static GetSubCategoryResponse AsSubCategoryResponseDto(this SubCategory subCategory)
        {
            return new GetSubCategoryResponse
            {
                CategoryId = subCategory.CategoryId,
                SubCategoryName = subCategory.Name,
                Id = subCategory.Id
            };
        }


        public static GetProductResponse AsProductResponseDto(this Product product)
        {
            return new GetProductResponse
            {
                ProductId = product.Id,
                ProductName = product.Name,
                ProductDescription = product.Description,
                ProductPrice = product.Price,
                Quantity = product.Quantity,
                CategoryName = product.Category.CategoryName,
                CategoryId = product.Category.Id,
                ShopName = product.Vendor.ShopName,
                ShopId = product.Vendor.Id,
                DateAdded = product.DateAdded
            };
        }


        public static CartItemResponse AsCartResponseDto(this CartItem cartItem)
        {
            return new CartItemResponse
            {
                CartItemId = cartItem.Id,
                ProductId = cartItem.ProductId,
                ProductName = cartItem.Product.Name,
                ProductDescription = cartItem.Product.Description,
                ProductUnitPrice = cartItem.Product.Price,
                TotalQuantityAvailable = cartItem.Product.Quantity,
                QuantityOfProductInCart = cartItem.Quantity,
                ProductImage = cartItem.Product.ImageUrl,
                CartId = cartItem.CartId
            };
        }


        public static PagedResponse<List<T>> CreatePagedReponse<T>(this List<T> pagedData, Filter validFilter, int totalRecords, IUriService uriService, string route)
        {
            var response = new PagedResponse<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);

            var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            //Generate link to next page
            response.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new Filter(validFilter.PageNumber + 1, validFilter.PageSize), route)
                : null;

            //Previous page link implementation    
            response.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new Filter(validFilter.PageNumber - 1, validFilter.PageSize), route)
                : null;

            //First and last page link
            response.FirstPage = uriService.GetPageUri(new Filter(1, validFilter.PageSize), route);
            response.LastPage = uriService.GetPageUri(new Filter(roundedTotalPages, validFilter.PageSize), route);
            
            response.TotalPages = roundedTotalPages;
            response.TotalRecords = totalRecords;
            return response;
        }
    }
}