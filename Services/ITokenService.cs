using AduabaNeptune.Data.Entities;

namespace AduabaNeptune.Services
{
    public interface ITokenService
    {
        string GenerateToken(Customer customer);
    }
}