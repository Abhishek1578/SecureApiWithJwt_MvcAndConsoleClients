using Microsoft.EntityFrameworkCore;
using SBIMFJWT.Models;

namespace SBIMFJWT.DATA
{
    public class ApplicationDbContext:DbContext
    {
        public  ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
