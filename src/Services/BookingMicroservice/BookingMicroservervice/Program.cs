using BookingMicroservervice.Data;
using BookingMicroservervice.DTOs;
using BookingMicroservervice.Enums;
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

            app.MapGet("/bookings/{id:int}", async (
            int id,
            ApplicationDbContext db) =>
                    {
                // Find booking by ID
                var booking = await db.Bookings.FindAsync(id);

                // Check if booking exists
                if (booking == null)
                {
                    return Results.NotFound(
                        $"Booking with ID {id} was not found.");
                }

                // Return booking
                return Results.Ok(booking);
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

           
            app.MapPut("/bookings/{id:int}", async (
                int id,
                ApplicationDbContext db,
                BookingDto dto) =>
            {
                // Find existing booking
                var existingBooking = await db.Bookings.FindAsync(id);

                if (existingBooking == null)
                {
                    return Results.NotFound(
                        $"Booking with ID {id} was not found.");
                }

                // Convert DTO to updated entity
                var updatedBooking = BookingMapper.ToEntity(dto);

                // Update properties
                existingBooking.PassengerId = updatedBooking.PassengerId;
                existingBooking.FlightId = updatedBooking.FlightId;
                existingBooking.BookingDate = updatedBooking.BookingDate;
                existingBooking.Status = updatedBooking.Status;

                // Save changes
                await db.SaveChangesAsync();

                return Results.Ok(existingBooking);
            });


            
            app.MapDelete("/bookings/{id:int}", async (
                int id,
                ApplicationDbContext db) =>
            {
                // Find booking
                var booking = await db.Bookings.FindAsync(id);

                if (booking == null)
                {
                    return Results.NotFound(
                        $"Booking with ID {id} was not found.");
                }

                // Remove booking
                db.Bookings.Remove(booking);

                // Save changes
                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            
            app.MapPatch("/bookings/{id:int}/cancel", async (
            int id,
            ApplicationDbContext db) =>
            {
                var booking = await db.Bookings.FindAsync(id);

                if (booking == null)
                {
                    return Results.NotFound(
                        $"Booking with ID {id} was not found.");
                }

                booking.Status = BookingStatus.Cancelled;

                await db.SaveChangesAsync();

                return Results.Ok(booking);
            });

            app.Run();
        }
    }
}
