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
    public class CardService : ICardService
    {
        private readonly ApplicationDbContext _context;

        public CardService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task DeleteCreditCardAsync(List<string> cardIds, int customerId)
        {
            List<Card> cardsToDelete = new List<Card>();
            cardsToDelete = await _context.Cards.Where(c => c.CustomerId == customerId && cardIds.Contains(c.Id)).ToListAsync();

            if (cardsToDelete.Count != 0)
            {
                _context.Cards.RemoveRange(cardsToDelete);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<List<Card>> GetAllCustomerCreditCardsAsync(int customerId)
        {
            List<Card> availableCards = new List<Card>();
            availableCards = await _context.Cards.Where(c => c.CustomerId == customerId).ToListAsync();

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


        public async Task<Card> GetCustomerCreditCardByIdAsync(string cardId)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(c => c.Id == cardId);

            if (card is null)
            {
                return null;
            }
            else
            {

                card.CardNumber = Decrypt(card.CardNumber);
                card.CCV = Decrypt(card.CCV);

                return card;
            }
        }


        public async Task<Card> SaveCreditCardAsync(SaveCardRequest card, int customerId)
        {
            //Check if card already exist using card number
            //Encrypt card number to use for search along with user id
            var encryptedCardNumber = Encrypt(card.CardNumber);

            var existingCard = await _context.Cards.FirstOrDefaultAsync(c => c.CustomerId == customerId && c.CardNumber == encryptedCardNumber);

            if(existingCard != null){return existingCard;}

            var creditCard = new Card
            {
                CardHolderName = card.CardHolderName,
                CardNumber = encryptedCardNumber,
                CCV = Encrypt(card.CCV),
                ExpiryDate = card.ExpiryDate,
                CustomerId = customerId,
                Id = Guid.NewGuid().ToString()
            };

            await _context.Cards.AddAsync(creditCard);
            await _context.SaveChangesAsync();
            return creditCard;
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