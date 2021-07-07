using System.Collections.Generic;
using System.Threading.Tasks;
using AduabaNeptune.Data.Entities;
using AduabaNeptune.Dto;

namespace AduabaNeptune.Services
{
    public interface IVendorService
    {
        Task<IEnumerable<Vendor>> GetVendors();
        Task<Vendor> GetVendorById(int vendorId);
        Task<Vendor> AddVendor(Vendor vendor);
        Task<Vendor> UpdateVendor(Vendor vendor);
        void DeleteVendor(int vendorId);

    }
}