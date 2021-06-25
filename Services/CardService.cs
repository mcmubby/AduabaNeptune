using System;
using System.Collections.Generic;
using System.Linq;
using AduabaNeptune.Data;
using AduabaNeptune.Data.Entities;
using AduabaNeptune.Dto;
using BCryptNet = BCrypt.Net.BCrypt;

namespace AduabaNeptune.Services
{
    public class CardService : ICardService
    {
        private readonly ApplicationDbContext _context;

        public CardService(ApplicationDbContext context)
        {
            _context = context;
        }


        public void DeleteCreditCard(List<Card> cards)
        {
            throw new System.NotImplementedException();
        }


        public List<Card> GetAllCreditCards()
        {
            throw new System.NotImplementedException();
        }
        

        public void SaveCreditCard(SaveCardRequest card, string customerId)
        {
            //Check if card already exist using card number
            //Encrypt card number to use for search along with user id
            //Id is being parsed to int because it was converted to string in claims during token generation
            var actualCustomerId = int.Parse(customerId);
            var encryptedCardNumber = EnryptString(card.CardNumber);

            var existingCard = _context.Cards.FirstOrDefault(c => c.CustomerId == actualCustomerId && c.CardNumber == encryptedCardNumber);

            if(existingCard != null){}

            var creditCard = new Card
            {
                CardHolderName = card.CardHolderName,
                CardNumber = encryptedCardNumber,
                CCV = card.CCV,
                ExpiryDate = card.ExpiryDate
            };
        }


        public static string DecryptString(string value)
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

        public  static string EnryptString(string value)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(value);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }
    }
}