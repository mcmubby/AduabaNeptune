using System.Collections.Generic;
using System.Threading.Tasks;
using AduabaNeptune.Dto;

namespace AduabaNeptune.Services
{
    public interface IShippingAddressService
    {
        Task<List<ShippingAddressResponse>> GetAllShippingAddressesAsync(int customerId);
        Task<ShippingAddressResponse> AddShippingAddressAsync(AddShippingAddressRequest request, int customerId);
        Task DeleteShippingAddress(List<int> shippingAddressIds, int customerId);
        Task<ShippingAddressResponse> GetShippingAddressById(int shippingAddressId, int customerId);

    }
}