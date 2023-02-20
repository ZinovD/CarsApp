using Microsoft.EntityFrameworkCore;

namespace CarsApp.Data.Models
{
    public class ApplicationContext: DbContext
    {
        public DbSet<CarModels> CarModels { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)=>Database.EnsureCreated();
    }
}
