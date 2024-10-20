using Microsoft.EntityFrameworkCore;

namespace WayFarer.Repository
{
    public class WayFarerDbContext : DbContext
    {
        public WayFarerDbContext(DbContextOptions<WayFarerDbContext> options) : base(options) { }


        
    }
}
