using Microsoft.EntityFrameworkCore;
using Production.API.Models;


namespace Production.API.Data
{
    public class ProductionContext : DbContext
    {
        public ProductionContext(DbContextOptions<ProductionContext> options)
: base(options)
        {
        }

        public DbSet<Activity> Products { get; set; }
    }
}
