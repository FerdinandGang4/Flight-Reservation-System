using PaymentMicroservice.Enum;

namespace PaymentMicroservice.Model
{
    public class Payment
    {
        public int Id { get; set; }
        public int PassengerId { get; set; }
        public int BookingId { get; set; }
        public Decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus Status { get; set; }
       
    }
}
