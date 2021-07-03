using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AduabaNeptune.Data;
using AduabaNeptune.Data.Entities;
using AduabaNeptune.Dto;
using Microsoft.EntityFrameworkCore;

namespace AduabaNeptune.Services
{
    public class VendorService : IVendorService
    {
        private readonly ApplicationDbContext _context;

        public VendorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vendor>> GetVendors()
        {
            return await _context.Vendors.ToListAsync();
        }

        public async Task<Vendor> GetVendorById(int vendorId)
        {
            return await _context.Vendors.FirstOrDefaultAsync(v => v.VendorId == vendorId);
        }
        public async Task<Vendor> AddVendor(Vendor vendor)
        {
            var result = await _context.Vendors.AddAsync(vendor);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Vendor> UpdateVendor(Vendor vendor)
        {
            var result = await _context.Vendors.FirstOrDefaultAsync(v => v.VendorId == vendor.VendorId);

            if (result ! = null)
            {
                await _context.SaveChangesAsync();
                return result;
            }

            return null;

        }

        public async void DeleteVendor(int vendorId)
        {
            var result = await _context.Vendors.FirstOrDefaultAsync(v => v.VendorId == vendorId);
            if (result ! = null)
            {
                _context.Vendors.Remove(result);
                await _context.SaveChangesAsync();

            }
        }
         
    }
}