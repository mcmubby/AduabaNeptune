using Microsoft.EntityFrameworkCore;

namespace AduabaNeptune.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

    
    }
}