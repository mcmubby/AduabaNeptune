using System.Threading.Tasks;
using AduabaNeptune.Dto;

namespace AduabaNeptune.Services
{
    public interface IAccountService
    {
        Task<bool> RegisterCustomerAsync(RegistrationRequest model);
        Task<string> SignInCustomerAsync(SignInRequest model);
        Task<string> UpdateCustomerDetailAsync(UpdateCustomerRequest model, string customerEmail);
    }
}