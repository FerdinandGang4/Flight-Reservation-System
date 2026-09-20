using BookingMicroservervice.Data;
using BookingMicroservervice.DTOs;
using BookingMicroservervice.Mapper;
using BookingMicroservervice.Model;
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

            app.MapGet("/bookings", async (ApplicationDbContext db) => {
            
               return await db.Bookings.ToListAsync();
            
            });

           

            app.MapPost("/bookings", async (
                   ApplicationDbContext db, BookingDto dto) =>
            {
                // Convert DTO to Booking entity
                var booking = BookingMapper.ToEntity(dto);

                // Save Booking entity to database
                await db.Bookings.AddAsync(booking);
                await db.SaveChangesAsync();

                return Results.Created($"/bookings/{booking.Id}", booking);
            });

            app.Run();
        }
    }
}
