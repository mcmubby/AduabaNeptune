using System;
using AduabaNeptune.Data;
using AduabaNeptune.Dto;
using AduabaNeptune.Data.Entities;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AduabaNeptune.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public AccountService(ApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<bool> RegisterCustomerAsync(RegistrationRequest model)
        {
            //Check if the email is already registered
            var alreadyRegistered = await _context.Customers.AnyAsync(c => c.Email == model.Email);

            if (alreadyRegistered)
            {
                return false;
            }
            else
            {
                var newCustomer = new Customer
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = BCryptNet.HashPassword(model.Password),
                    DateCreated = DateTime.UtcNow
                };

                await _context.Customers.AddAsync(newCustomer);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<string> SignInCustomerAsync(SignInRequest model)
        {
            var existingCustomer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == model.Email);

            if (existingCustomer == null || !BCryptNet.Verify(model.Password, existingCustomer.Password))
            {
                return null;
            }
            else
            {
                var token = _tokenService.GenerateToken(existingCustomer);
                return token;
            }
        }

        public async Task<string> UpdateCustomerDetailAsync(UpdateCustomerRequest model, string customerEmail )
        {
            
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == customerEmail);

            if (customer is null)
            {
                return null;
            }
            else
            {
                customer.Email = model.Email;
                customer.FirstName = model.FirstName;
                customer.LastName = model.LastName;
                customer.LastModified = DateTime.UtcNow;

                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();

                //Generate new token
                var token = _tokenService.GenerateToken(customer);
                return token;
            }
        }
    }
}