using Microsoft.EntityFrameworkCore;
using PaymentMicroservice.Data;

namespace PaymentMicroservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ApplicationDbContext>(option =>
             option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.Run();
        }
    }
}
