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
           
            //builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(option =>
            option.UseNpgsql(builder.Configuration.GetConnectionString("PassengerDbConnectionString")));


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            //Creating API end points
            app.MapGet("/passengers", async (ApplicationDbContext db) => {

                var passenger = await db.Passengers.ToListAsync();

                return Results.Ok(passenger);
            });

            //Get a single passenger by id
            app.MapGet("/passengers/{id}", async (ApplicationDbContext db, int id) => {

                var passenger = await db.Passengers.FindAsync(id);

                if(passenger == null)
                {
                    return Results.NotFound($"There is no Passenger with id {id}");
                }

                return Results.Ok(passenger);
            });

            //This is to create a Passenger object
            app.MapPost("/passengers", async (ApplicationDbContext db, Pessenger passenger) =>
            {
              
                if(passenger == null)
                {
                    return Results.BadRequest($"Please make sure all required information are entered");
                }

                await db.Passengers.AddAsync(passenger);

                await db.SaveChangesAsync();

                return Results.Created($" new passenger created with id {passenger.Id}", passenger);
            });

            //delete a passenger from the system
            app.MapDelete("/passengers/{id}", async (ApplicationDbContext db, int id) => {

                var passenger = await db.Passengers.FindAsync(id);

                if (passenger == null)
                {
                    return Results.NotFound($"There is no passenger with id {id}");
                }

                db.Passengers.Remove(passenger);

                await db.SaveChangesAsync();

                return Results.Ok($"passenger with id {id} deleted");
            });

            app.MapPut("/passengers/{id}", async (ApplicationDbContext db, int id, Pessenger passenger) =>
            {

                var currentPassenger = await db.Passengers.FindAsync(id);
                if (currentPassenger == null)
                {
                    return Results.NotFound($"There is no passenger with id {id}");
                }

                currentPassenger.Id = passenger.Id;
                currentPassenger.Email = passenger.Email;
                currentPassenger.LastName = passenger.LastName;
                currentPassenger.FirstName = passenger.FirstName;
                currentPassenger.PassportNumber = passenger.PassportNumber;

                await db.SaveChangesAsync();

                return Results.Ok($"Successfully updated passenger with id {id}");

            });

            app.Run();
        }
    }
}
