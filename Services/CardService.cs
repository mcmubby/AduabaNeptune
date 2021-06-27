using System;
using System.Collections.Generic;
using System.Linq;
using AduabaNeptune.Data;
using AduabaNeptune.Data.Entities;
using AduabaNeptune.Dto;

namespace AduabaNeptune.Services
{
    public class CardService : ICardService
    {
        private readonly ApplicationDbContext _context;

        public CardService(ApplicationDbContext context)
        {
            _context = context;
        }


        public void DeleteCreditCard(List<string> cardIds, int customerId)
        {
            List<Card> cardsToDelete = new List<Card>();
            cardsToDelete = _context.Cards.Where(c => c.CustomerId == customerId && cardIds.Contains(c.Id)).ToList();

            if (cardsToDelete.Count != 0)
            {
                _context.Cards.RemoveRange(cardsToDelete);
                _context.SaveChanges();
            }
        }


        public List<Card> GetAllCustomerCreditCards(int customerId)
        {
            List<Card> availableCards = new List<Card>();
            availableCards = _context.Cards.Where(c => c.CustomerId == customerId).ToList();

            if (availableCards.Count == 0)
            {
                return null;
            }
            else
            {
                for (int i = 0; i < availableCards.Count; i++)
                {
                    availableCards[i].CardNumber = Decrypt(availableCards[i].CardNumber);
                    availableCards[i].CCV = Decrypt(availableCards[i].CCV);

                }
                return availableCards;
            }
            
            
        }


        public bool SaveCreditCard(SaveCardRequest card, int customerId)
        {
            //Check if card already exist using card number
            //Encrypt card number to use for search along with user id
            var encryptedCardNumber = Encrypt(card.CardNumber);

            var existingCard = _context.Cards.FirstOrDefault(c => c.CustomerId == customerId && c.CardNumber == encryptedCardNumber);

            if(existingCard != null){return false;}

            var creditCard = new Card
            {
                CardHolderName = card.CardHolderName,
                CardNumber = encryptedCardNumber,
                CCV = Encrypt(card.CCV),
                ExpiryDate = card.ExpiryDate,
                CustomerId = customerId,
                Id = Guid.NewGuid().ToString()
            };

            _context.Cards.Add(creditCard);
            _context.SaveChanges();
            return true;
        }


        public static string Decrypt(string value)
        {
            byte[] b;
            string decrypted;
            try
            {
                b = Convert.FromBase64String(value);
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
            }
            catch (FormatException)
            {
                decrypted = "";
            }
            return decrypted;
        }

        public  static string Encrypt(string value)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(value);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }
    }
}