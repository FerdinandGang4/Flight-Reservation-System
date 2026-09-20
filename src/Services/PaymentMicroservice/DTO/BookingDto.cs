namespace PaymentMicroservice.DTO
{
    public class BookingDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime BookingDate { get; set; }
        public PassengerDto Passenger { get; set; }
       
    }
}
