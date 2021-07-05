using AduabaNeptune.Data.Entities;

namespace AduabaNeptune.Services
{
    public interface ITokenService
    {
        string GenerateCustomerToken(Customer customer);
        string GenerateEmployeeToken(Employee employee);
        string GenerateVendorToken(Vendor vendor);
    }
}