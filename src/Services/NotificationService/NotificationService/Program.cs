using Microsoft.EntityFrameworkCore;
using NotificationService.Data;
using NotificationService.DTOs;
using NotificationService.Mapper;
using NotificationService.Models;

namespace NotificationService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseNpgsql(builder.Configuration.GetConnectionString("" +
                "NotificationServiceConnectionString")));

            builder.Services.AddSwaggerGen();

            // Add services to the container.

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //To get the list of all notifications
            app.MapGet("/notifications", async (ApplicationDbContext db) => { 
            
               return await db.Notifications.ToListAsync();
            });


            // To get a single notification by id
            app.MapGet("/notifications{id}", async (ApplicationDbContext db, int id) => {

            var notification = await db.Notifications.FindAsync(id);

                if (notification == null) {

                    return Results.BadRequest($"There is no notification for the id {id}");

                }

                var dto = NotificationMapper.ToDto(notification);

                return Results.Ok(dto);
            });


            //To create notification
            app.MapPost("/notifications", async (ApplicationDbContext db, NotificationDto dto) =>
            {
           
               var notification = NotificationMapper.ToEntity(dto);

               await db.Notifications.AddAsync(notification);

               await db.SaveChangesAsync();

               return Results.Created($"Created notification with id {notification.Id}", notification);

            });


            // To delete notification
            app.MapDelete("/notifications/{id}", async (ApplicationDbContext db, int id) => {

                var notification = await db.Notifications.FindAsync(id);

                if (notification == null)
                {

                    return Results.BadRequest($"There is no notification for the id {id}");

                }

                 db.Notifications.Remove(notification);

                await db.SaveChangesAsync();

                return Results.Ok($"Successfully deleted notification with id {id}");
            });


            //To update notication

            app.MapPut("notifications/{id}", async (NotificationDto dto, int id, ApplicationDbContext db) => {
            
               if(dto == null)
                {
                    return Results.NotFound($"Please enter the correctect information");
                }

                Notification updatedNotification = await db.Notifications.FindAsync(id);

               
                updatedNotification.Message = dto.Message;
                updatedNotification.NotificationDate = dto.NotificationDate;
                updatedNotification.BookingId = dto.BookingId;
                updatedNotification.PassengerId = dto.PassengerId;

               await db.SaveChangesAsync();

                return Results.Ok($"Successfully updated notification with id {id}");

            });

            app.Run();
        }
    }
}
