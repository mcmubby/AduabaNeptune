using AduabaNeptune.Dto;

namespace AduabaNeptune.Services
{
    public interface IAccountService
    {
        void RegisterCustomer(RegistrationRequest model);
        void SignInCustomer(SignInRequest model);
        void UpdateCustomerDetail(UpdateCustomerRequest model);
    }
}