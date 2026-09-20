using Microsoft.EntityFrameworkCore;
using BookingMicroservervice.Model;

namespace BookingMicroservervice.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<Booking> Bookings { get; set; }
    }
}
