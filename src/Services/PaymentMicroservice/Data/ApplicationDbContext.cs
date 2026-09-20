using Microsoft.EntityFrameworkCore;
using PaymentMicroservice.Model;

namespace PaymentMicroservice.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<Payment> Payments { get; set; }
    }
}
