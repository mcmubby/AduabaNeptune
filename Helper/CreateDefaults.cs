using System;
using System.Linq;
using AduabaNeptune.Data;
using AduabaNeptune.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BCryptNet = BCrypt.Net.BCrypt;


namespace AduabaNeptune
{
    public static class CreateDefaults
    {
        public static void MigrateDatabaseContext(IServiceProvider svp)
        {
            var applicationDbContext = svp.GetRequiredService<ApplicationDbContext>();
            applicationDbContext.Database.Migrate();
        }


        public static void CreateDefaultAdmin(IServiceProvider svp)
        {
            var adminEmail = "admin@abuaba.com";
            var adminPassword = "adminPassword";

            var dbContext = svp.GetRequiredService<ApplicationDbContext>();
            var admin = dbContext.Employees.Where(e => e.OfficialEmail == adminEmail).FirstOrDefault();
            if(admin is null)
            {
                admin = new Employee
                {
                    OfficialEmail = adminEmail,
                    Password = adminPassword,
                    Role = "admin",
                    FirstName = "Admin",
                    LastName = "Aduaba",
                    DateCreated = DateTime.UtcNow
                };

                dbContext.Employees.Add(admin);
                dbContext.SaveChanges();
            }
        }



        public static void CreateDefaultVendor(IServiceProvider svp)
        {
            var vendorEmail = "admin@abuaba.com";
            var vendorPassword = "adminPassword";

            var dbContext = svp.GetRequiredService<ApplicationDbContext>();
            var vendor = dbContext.Vendors.Where(e => e.Email == vendorEmail).FirstOrDefault();

            if(vendor is null)
            {
                vendor = new Vendor
                {
                    Email = vendorEmail,
                    Password = vendorPassword,
                    ShopName = "Aduaba",
                    VendorName = "Aduaba Admin",
                    DateJoined = DateTime.UtcNow,
                    PhoneNumber = "+23412345678"
                };

                dbContext.Vendors.Add(vendor);
                dbContext.SaveChanges();
            }
        }
    }
}