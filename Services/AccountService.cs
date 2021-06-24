using System.Security.Claims;
using System;
using System.Linq;
using AduabaNeptune.Data;
using AduabaNeptune.Dto;
using AduabaNeptune.Data.Entities;
using BCryptNet = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Http;

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

        public bool RegisterCustomer(RegistrationRequest model)
        {
            //Check if the email is already registered
            var alreadyRegistered = _context.Customers.Any(c => c.Email == model.Email);

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

                _context.Customers.Add(newCustomer);
                _context.SaveChanges();
                return true;
            }
        }

        public string SignInCustomer(SignInRequest model)
        {
            var existingCustomer = _context.Customers.FirstOrDefault(c => c.Email == model.Email);

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

        public string UpdateCustomerDetail(UpdateCustomerRequest model, Claim customerClaim )
        {
            //Verify that Customer claim is not empty so ef doesn't throw error while searching
            if (string.IsNullOrEmpty(customerClaim.Value))
            {
                return null;
            }

            var customer = _context.Customers.FirstOrDefault(c => c.Email == customerClaim.Value);

            if (customer == null)
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
                _context.SaveChanges();

                //Generate new token
                var token = _tokenService.GenerateToken(customer);
                return token;
            }
        }
    }
}