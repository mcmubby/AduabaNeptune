using AduabaNeptune.Data;
using AduabaNeptune.Dto;

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
        
        public void RegisterCustomer(RegistrationRequest model)
        {
            throw new System.NotImplementedException();
        }

        public void SignInCustomer(SignInRequest model)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCustomerDetail(UpdateCustomerRequest model)
        {
            throw new System.NotImplementedException();
        }
    }
}