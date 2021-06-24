using AduabaNeptune.Model;

namespace AduabaNeptune.Services
{
    public interface ITokenService
    {
        string GenerateToken(Customer customer);
    }
}