using AduabaNeptune.Data.Entities;
using AduabaNeptune.Dto;

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
                CCV = card.CCV
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
    }
}