using BookingMicroservervice.Enums;


namespace BookingMicroservervice.Model
{
    public class Booking
    {
        public int Id { get; set; }
        public int PassengerId { get; set; }
        public int FlightId { get; set; }
        public Decimal Price { get; set; }
        public DateTime BookingDate { get; set; }
        public BookingStatus Status { get; set; }

    }
}
