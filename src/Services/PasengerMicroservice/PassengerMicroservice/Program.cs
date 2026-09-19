using Microsoft.EntityFrameworkCore;
using PassengerMicroservice.Model;
using PassengerMicroservice.Data;

namespace PassengerMicroservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(option =>
            option.UseNpgsql(builder.Configuration.GetConnectionString("PassengerDbConnectionString")));


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

           
           

            app.Run();
        }
    }
}
