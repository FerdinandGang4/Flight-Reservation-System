using NotificationService.DTOs;
using NotificationService.Models;

namespace NotificationService.Mapper
{
    public static class NotificationMapper
    {
        //To entity
        public static Notification ToEntity(NotificationDto dto)
        {
            return new Notification
            {
                Id = dto.Id,
                BookingId = dto.BookingId,
                Message = dto.Message,
                NotificationDate = dto.NotificationDate,
                PassengerId = dto.PassengerId
            };
        }

        //To Dto
        public static NotificationDto ToDto(Notification entity)
        {
            return new NotificationDto
            {
                Id = entity.Id,
                BookingId = entity.BookingId,
                PassengerId = entity.PassengerId,
                Message = entity.Message,
                NotificationDate = entity.NotificationDate
            };

        }
    }
}

