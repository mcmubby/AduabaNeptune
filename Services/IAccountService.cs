using System.Security.Claims;
using AduabaNeptune.Dto;

namespace AduabaNeptune.Services
{
    public interface IAccountService
    {
        bool RegisterCustomer(RegistrationRequest model);
        string SignInCustomer(SignInRequest model);
        string UpdateCustomerDetail(UpdateCustomerRequest model, Claim customerClaim);
    }
}