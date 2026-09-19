namespace BookingMicroservervice.DTOs
{
    public class FlightDto
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public string OriginalLocation { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
    }
}
