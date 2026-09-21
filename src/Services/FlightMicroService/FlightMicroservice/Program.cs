using FlightMicroservice.Data;
using FlightMicroservice.Model;
using Microsoft.EntityFrameworkCore;
// Keep your existing using statements for ApplicationDbContext and Flight

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // =========================
        // Register Services
        // =========================

        builder.Services.AddAuthorization();

        // PostgreSQL database
        builder.Services.AddDbContext<ApplicationDbContext>(
            options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                )
        );

        //// =========================
        //// CORS Configuration
        //// =========================
        //builder.Services.AddCors(options =>
        //{
        //    options.AddPolicy("ReactApp", policy =>
        //    {
        //        policy
        //            .WithOrigins("http://localhost:5173")
        //            .AllowAnyHeader()
        //            .AllowAnyMethod();
        //    });
        //});

        //// Swagger
        //builder.Services.AddSwaggerGen();

        var app = builder.Build();


        // =========================
        // Swagger
        // =========================
        //if (app.Environment.IsDevelopment())
        //{
        //    app.UseSwagger();
        //    app.UseSwaggerUI();
        //}


        // =========================
        // Middleware
        // =========================

        app.UseHttpsRedirection();

        // IMPORTANT: Enable the CORS policy
        //app.UseCors("ReactApp");

        app.UseAuthorization();


        // =========================
        // FLIGHT ENDPOINTS
        // =========================


        // GET all flights
        app.MapGet("/flights", async (ApplicationDbContext db) =>
        {
            return await db.Flights.ToListAsync();
        });


        // GET flight by ID
        app.MapGet("/flights/{id}", async (
            ApplicationDbContext db,
            int id) =>
        {
            var flight = await db.Flights.FindAsync(id);

            if (flight == null)
            {
                return Results.NotFound("Flight not found");
            }

            return Results.Ok(flight);
        });


        // CREATE flight
        app.MapPost("/flights", async (
            Flight flight,
            ApplicationDbContext db) =>
        {
            if (flight == null)
            {
                return Results.BadRequest(
                    "Please provide valid flight information"
                );
            }

            await db.Flights.AddAsync(flight);
            await db.SaveChangesAsync();

            return Results.Created(
                $"/flights/{flight.Id}",
                flight
            );
        });


        // DELETE flight
        app.MapDelete("/flights/{id}", async (
            ApplicationDbContext db,
            int id) =>
        {
            var flight = await db.Flights.FindAsync(id);

            if (flight == null)
            {
                return Results.NotFound(
                    $"There is no flight with id {id}"
                );
            }

            db.Flights.Remove(flight);

            await db.SaveChangesAsync();

            return Results.Ok(
                $"Flight with id {flight.Id} was deleted successfully"
            );
        });


        // UPDATE flight
        app.MapPut("/flights/{id}", async (
            Flight flight,
            ApplicationDbContext db,
            int id) =>
        {
            var currentFlight = await db.Flights.FindAsync(id);

            if (currentFlight == null)
            {
                return Results.NotFound(
                    $"There is no flight with id {id}"
                );
            }

            currentFlight.FlightNumber = flight.FlightNumber;
            currentFlight.OriginalLocation = flight.OriginalLocation;
            currentFlight.Destination = flight.Destination;
            currentFlight.AvailableSeats = flight.AvailableSeats;
            currentFlight.Capacity = flight.Capacity;
            currentFlight.DepartureTime = flight.DepartureTime;

            await db.SaveChangesAsync();

            return Results.Ok(
                $"Successfully updated flight number {id}"
            );
        });


        app.Run();
    }
}