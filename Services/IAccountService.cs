using System.Threading.Tasks;
using AduabaNeptune.Data.Entities;
using AduabaNeptune.Dto;

namespace AduabaNeptune.Services
{
    public interface IAccountService
    {
        Task<Customer> RegisterCustomerAsync(RegistrationRequest model);
        Task<string> SignInCustomerAsync(SignInRequest model);
        Task<string> UpdateCustomerDetailAsync(UpdateCustomerRequest model, string customerEmail);
        Task<bool> RegisterVendorAsync(RegistrationRequest model);
        Task<string> SignInVendorAsync(SignInRequest model);
        Task<string> UpdateVendorDetailAsync(UpdateCustomerRequest model, string vendorEmail);
        Task<string> SignInEmployeeAsync(SignInRequest model);
        Task<Customer> GetCustomerByEmail(string emailAddress);
        Task<bool> VerifyResetPinAsync(int pin, string emailAddress);
        Task<bool> UpdateCustomerPassword(string email, string newPassword);
    }
}