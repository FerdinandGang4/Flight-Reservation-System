using FlightMicroservice.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightMicroservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddSwaggerGen();
            //builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if( app.Environment.IsDevelopment()){
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();

         
            //app.MapGet("/flights", async (ApplicationDbContext db) =>
            //{
            //    return await db.Flights.ToListAsync();
            //});

            app.Run();
        }
    }
}
