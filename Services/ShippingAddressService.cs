using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AduabaNeptune.Data;
using AduabaNeptune.Data.Entities;
using AduabaNeptune.Dto;
using AduabaNeptune.Helper;
using Microsoft.EntityFrameworkCore;

namespace AduabaNeptune.Services
{
    public class ShippingAddressService : IShippingAddressService
    {
        private readonly ApplicationDbContext _context;

        public ShippingAddressService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ShippingAddressResponse> AddShippingAddressAsync(AddShippingAddressRequest request, int customerId)
        {
            var shippingDetails = new ShippingAddress
            {
                ContactPersonsName = request.ContactPersonsName,
                Address = request.Address,
                City = request.City == null ? "" : request.City,
                PhoneNumber = request.PhoneNumber,
                AlternatePhoneNumber = request.AlternatePhoneNumber == null ? "" : request.AlternatePhoneNumber,
                Landmark = request.Landmark == null ? "" : request.Landmark,
                CustomerId = customerId
            };

            _context.ShippingAddresses.Add(shippingDetails);
            await _context.SaveChangesAsync();
            return shippingDetails.AsShippingAddressResponseDto();
        }

        public async Task DeleteShippingAddress(List<int> shippingAddressIds, int customerId)
        {
            var shippingAddresses = new List<ShippingAddress>();
            shippingAddresses = await _context.ShippingAddresses.Where(s => s.CustomerId == customerId && shippingAddressIds.Contains(s.Id))
                                                                .ToListAsync();

            if (shippingAddresses.Count != 0)
            {
                _context.ShippingAddresses.RemoveRange(shippingAddresses);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ShippingAddressResponse>> GetAllShippingAddressesAsync(int customerId)
        {
            var response = new List<ShippingAddressResponse>();
            var shippingAddresses = await _context.ShippingAddresses.Where(s => s.CustomerId == customerId)
                                                                    .ToListAsync();
            
            if(shippingAddresses.Count != 0)
            {
                foreach (var address in shippingAddresses)
                {
                    response.Add(address.AsShippingAddressResponseDto());
                }
            }

            return response;
        }

        public async Task<ShippingAddressResponse> GetShippingAddressById(int shippingAddressId, int customerId)
        {
            var shippingAddress = await _context.ShippingAddresses.Where(s => s.CustomerId == customerId && s.Id == shippingAddressId)
                                                                  .FirstOrDefaultAsync();
            
            if(shippingAddress != null){return shippingAddress.AsShippingAddressResponseDto();}

            return null;
        }
    }
}