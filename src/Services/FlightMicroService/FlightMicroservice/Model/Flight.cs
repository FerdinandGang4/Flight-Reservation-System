namespace FlightMicroservice.Model
{
    public class Flight
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public int Capacity { get; set; }
        public int AvailableSeats { get; set; }
        public string  OriginalLocation { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
    }
}
