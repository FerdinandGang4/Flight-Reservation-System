using BookingMicroservervice.DTOs;
using BookingMicroservervice.Enums;
using BookingMicroservervice.Model;


namespace BookingMicroservervice.Mapper
{
    public class BookingMapper
    {
        // DTO -> Entity
        public static Booking ToEntity(BookingDto dto)
        {
            return new Booking
            {
                PassengerId = dto.Passenger.Id,
                FlightId = dto.Flight.Id,
                Price = dto.Price,
                BookingDate = DateTime.UtcNow,
                Status = BookingStatus.Inprogress
            };
        }

        // Entity + external data -> DTO
        public static BookingDto ToDto(
            Booking booking,
            PassengerDto passenger,
            FlightDto flight)
        {
            return new BookingDto
            {
                Id = booking.Id,
                Price = booking.Price,
                BookingDate = booking.BookingDate,
                Status = booking.Status,

                Passenger = passenger,
                Flight = flight
            };
        }
    }
}
