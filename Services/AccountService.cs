using System;
using AduabaNeptune.Data;
using AduabaNeptune.Dto;
using AduabaNeptune.Data.Entities;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AduabaNeptune.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly IImageService _imageService;

        public AccountService(ApplicationDbContext context, ITokenService tokenService, IImageService imageService)
        {
            _context = context;
            _tokenService = tokenService;
            _imageService = imageService;
        }

        public async Task<Customer> GetCustomerByEmail(string emailAddress)
        {
            var existingCustomer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == emailAddress);

            if(existingCustomer == null){return null;}

            return existingCustomer;
        }

        public async Task<Customer> RegisterCustomerAsync(RegistrationRequest model)
        {
            //Check if the email is already registered
            var alreadyRegistered = await _context.Customers.AnyAsync(c => c.Email == model.Email);

            if (alreadyRegistered)
            {
                return null;
            }
            else
            {
                var newCustomer = new Customer
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = BCryptNet.HashPassword(model.Password),
                    DateCreated = DateTime.UtcNow,
                    PhoneNumber = model.PhoneNumber,
                    AvatarUrl = "avatar"
                };

                await _context.Customers.AddAsync(newCustomer);
                await _context.SaveChangesAsync();
                return newCustomer;
            }
        }

        public async Task<bool> RegisterVendorAsync(RegistrationRequest model)
        {
            var alreadyRegistered = await _context.Vendors.AnyAsync(c => c.Email == model.Email);

            if (alreadyRegistered)
            {
                return false;
            }
            else
            {
                var newVendor = new Vendor
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = BCryptNet.HashPassword(model.Password),
                    DateJoined = DateTime.UtcNow
                };

                await _context.Vendors.AddAsync(newVendor);
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
                var token = _tokenService.GenerateCustomerToken(existingCustomer);
                return token;
            }
        }

        public async Task<string> SignInEmployeeAsync(SignInRequest model)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(c => c.OfficialEmail == model.Email);

            if (employee == null || !BCryptNet.Verify(model.Password, employee.Password))
            {
                return null;
            }
            else
            {
                var token = _tokenService.GenerateEmployeeToken(employee);
                return token;
            }
        }

        public async Task<string> SignInVendorAsync(SignInRequest model)
        {
            var existingVendor = await _context.Vendors.FirstOrDefaultAsync(c => c.Email == model.Email);

            if (existingVendor == null || !BCryptNet.Verify(model.Password, existingVendor.Password))
            {
                return null;
            }
            else
            {
                var token = _tokenService.GenerateVendorToken(existingVendor);
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
                if(!string.IsNullOrEmpty(model.Base64ImageString))
                {
                    var upload = await _imageService.UploadCustomerAvatar(model.Base64ImageString);
                    if(!string.IsNullOrEmpty(upload)){customer.AvatarUrl = upload;}
                }
                customer.PhoneNumber = model.PhoneNumber;
                customer.FirstName = model.FirstName;
                customer.LastName = model.LastName;
                customer.LastModified = DateTime.UtcNow;

                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();

                //Generate new token
                var token = _tokenService.GenerateCustomerToken(customer);
                return token;
            }
        }

        public async Task<bool> UpdateCustomerPassword(string email, string newPassword)
        {
            var customer = await _context.Customers.Where(c => c.Email == email).FirstOrDefaultAsync();

            if(customer == null){return false;}

            customer.Password = BCryptNet.HashPassword(newPassword);
            customer.LastModified = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public Task<string> UpdateVendorDetailAsync(UpdateCustomerRequest model, string vendorEmail)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> VerifyResetPinAsync(int pin, string emailAddress)
        {
            var customer = await GetCustomerByEmail(emailAddress);

            if(customer == null){return false;}

            var savedPin = await _context.ResetPins.Where(p => p.CustomerId == customer.Id && DateTime.Now < p.ExpiresAt).FirstOrDefaultAsync();

            if(savedPin  == null){return false;}

            if(savedPin.Pin == pin)
            {
                _context.ResetPins.Remove(savedPin);
                _context.SaveChanges();
                return true;
            }
            
            return false;

        }
    }
}