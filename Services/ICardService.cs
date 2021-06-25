using System.Collections.Generic;
using AduabaNeptune.Data.Entities;
using AduabaNeptune.Dto;

namespace AduabaNeptune.Services
{
    public interface ICardService
    {
        void SaveCreditCard(SaveCardRequest card, string CustomerId);
        List<Card> GetAllCreditCards();
        void DeleteCreditCard(List<Card> cards);
    }
}