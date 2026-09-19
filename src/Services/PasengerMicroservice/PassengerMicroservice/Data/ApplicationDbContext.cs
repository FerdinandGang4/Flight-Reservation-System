using Microsoft.EntityFrameworkCore;
using PassengerMicroservice.Model;

namespace PassengerMicroservice.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<Pessenger> Passengers { get; set; }
    }
}
