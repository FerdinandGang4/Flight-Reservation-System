using FlightMicroservice.Data;
using FlightMicroservice.Model;
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

            builder.Services.AddDbContext<ApplicationDbContext>(
               options =>
               options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddSwaggerGen();
           


            var app = builder.Build();

            // ========================
            // Seed initial flight data
            // ========================
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider
                              .GetRequiredService<ApplicationDbContext>();

                // Only seed when the table is empty
                if (!db.Flights.Any())
                {
                    db.Flights.AddRange(
                        new Flight
                        {
                            FlightNumber = "AA101",
                            Capacity = 180,
                            AvailableSeats = 180,
                            OriginalLocation = "Dallas",
                            Destination = "New York",
                            DepartureTime = DateTime.UtcNow.AddDays(1)
                        },

                        new Flight
                        {
                            FlightNumber = "UA202",
                            Capacity = 200,
                            AvailableSeats = 200,
                            OriginalLocation = "Houston",
                            Destination = "Chicago",
                            DepartureTime = DateTime.UtcNow.AddDays(2)
                        },

                        new Flight
                        {
                            FlightNumber = "DL303",
                            Capacity = 150,
                            AvailableSeats = 150,
                            OriginalLocation = "Atlanta",
                            Destination = "Miami",
                            DepartureTime = DateTime.UtcNow.AddDays(3)
                        }
                    );

                    db.SaveChanges();
                }
            }

            //  Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();

            // This is to get all the available flights in the system
            app.MapGet("/flights", async (ApplicationDbContext db) =>
            {
                return await db.Flights.ToListAsync();
            });

            //To get a specific flight using its id
            app.MapGet("/flights/{id}", async (ApplicationDbContext db, int id) =>
            {
                var flight = await db.Flights.FindAsync(id);

                if(flight == null)
                {
                    return Results.NotFound("Flight not found");
                }

                return Results.Ok(flight);
            });

            //This is to create a flihgt
            app.MapPost("/flights", async (Flight flight, ApplicationDbContext db) =>
            {

            if (flight == null)
            {
                    return Results.BadRequest("Please provide valide flight information");

            }

             await db.Flights.AddAsync(flight);
             await db.SaveChangesAsync();

             return Results.Created($"/flight/{flight.Id}", flight);

            });

            app.MapDelete("/flights/{id}", async (ApplicationDbContext db, int id) => {

                var flight = await db.Flights.FindAsync(id);

                if(flight == null)
                {
                    return Results.NotFound($"There is no flight with id /{id}");
                }

                db.Flights.Remove(flight);
                await db.SaveChangesAsync();

                return Results.Ok($"Flight with id {flight.Id} was deleted successfully");
            
            });

            app.MapPut("flights/{id}", async (Flight flight, ApplicationDbContext db, int id) =>
            {
                var currentFlight = await db.Flights.FindAsync(id);
                if(currentFlight == null)
                {
                    return Results.NotFound($"The is no flight with id {id}"); 
                }

                currentFlight.FlightNumber = flight.FlightNumber;
                currentFlight.OriginalLocation = flight.OriginalLocation;
                currentFlight.Destination = flight.Destination;
                currentFlight.AvailableSeats = flight.AvailableSeats;
                currentFlight.Capacity= flight.Capacity;
                currentFlight.DepartureTime = flight.DepartureTime;


                //save the changes to the database
                await db.SaveChangesAsync();

                return Results.Ok($"Successfully updated flihgt number {id}");
            });

            app.Run();
        }
    }
}
