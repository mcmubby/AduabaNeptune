using System;
using System.Threading.Tasks;
using AduabaNeptune.Data;
using AduabaNeptune.Data.Entities;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace AduabaNeptune.Services
{
    public class MessageSenderService : IMessageSenderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public MessageSenderService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public void SendOrderComfirmationEmail(string emailAddress)
        {
            throw new System.NotImplementedException();
        }

        public void SendOrderConfirmationSms(string phoneNumber)
        {
            throw new System.NotImplementedException();
        }

        public async Task SendPasswordResetTokenEmail(string emailAddress, int customerId)
        {
            var random = new Random();
            int pin = random.Next(1000, 9999);
            string content = $"<p>Your password reset pin is : </p>" + pin.ToString();

            var subject = "Security Code";

            var apiKey = _configuration["SendGridAPIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("noreply.accountservice@aduabafresh.com", "ADUABA Customer Service");
            var to = new EmailAddress(emailAddress);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, pin.ToString(), content);

            await client.SendEmailAsync(msg).ConfigureAwait(false);

            var resetPin = new ResetPin{
                CustomerId = customerId,
                Pin = pin
            };

            await _context.ResetPins.AddAsync(resetPin);
            _context.SaveChanges();
        }

        public async Task SendPasswordResetTokenSms(string phoneNumber, int customerId)
        {
            var random = new Random();
            int pin = random.Next(3200, 9999);
            string content = $"Your password reset pin is : " + pin.ToString();


            var accountSid = _configuration["Twilio:AccountSid"];
            var authToken = _configuration["Twilio:Token"];
            TwilioClient.Init(accountSid, authToken);

            var to = new PhoneNumber($"+234{phoneNumber}");
            var from = new PhoneNumber("+12056702239");

            var message = MessageResource.Create(to: to, from: from, body: content);

            //Save pin to db on reset table
            var resetPin = new ResetPin{
                CustomerId = customerId,
                Pin = pin
            };

            await _context.ResetPins.AddAsync(resetPin);
            _context.SaveChanges();
        }
    }
}