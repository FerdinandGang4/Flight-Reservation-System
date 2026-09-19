using BookingMicroservervice.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingMicroservervice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(option=>
            option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddSwaggerGen();
            // Add services to the container.
          
            var app = builder.Build();

            if(app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            

            app.Run();
        }
    }
}
