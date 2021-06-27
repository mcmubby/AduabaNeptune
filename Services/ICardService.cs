using System.Collections.Generic;
using System.Threading.Tasks;
using AduabaNeptune.Data.Entities;
using AduabaNeptune.Dto;

namespace AduabaNeptune.Services
{
    public interface ICardService
    {
        Task<Card> SaveCreditCardAsync(SaveCardRequest card, int CustomerId);
        Task<List<Card>> GetAllCustomerCreditCardsAsync(int customerId);
        Task DeleteCreditCardAsync(List<string> cardIds, int customerId);
        Task<Card> GetCustomerCreditCardByIdAsync(string cardId);
    }
}