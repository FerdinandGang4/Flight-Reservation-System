using BookingMicroservervice.Enums;

namespace BookingMicroservervice.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime BookingDate { get; set; }
        public BookingStatus Status { get; set; }
        public PassengerDto Passenger { get; set; }
        public FlightDto Flight { get; set; }
    }
}
