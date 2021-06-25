using System.Collections.Generic;
using AduabaNeptune.Data.Entities;
using AduabaNeptune.Dto;

namespace AduabaNeptune.Services
{
    public interface ICardService
    {
        bool SaveCreditCard(SaveCardRequest card, int CustomerId);
        List<Card> GetAllCustomerCreditCards(int customerId);
        void DeleteCreditCard(List<string> cardIds, int customerId);
    }
}