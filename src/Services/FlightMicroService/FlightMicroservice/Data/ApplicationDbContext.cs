
using FlightMicroservice.Model;
using Microsoft.EntityFrameworkCore;

namespace FlightMicroservice.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {

        }

        //add db set properties
        public DbSet<Flight> Flights { get; set; }
    }
}
