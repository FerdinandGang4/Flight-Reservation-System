using PaymentMicroservice.Model;
using PaymentMicroservice.DTO;

namespace PaymentMicroservice.Mapper
{
    public class PaymentMapper
    {
        public static Payment ToEntity(PaymentDto dto)
        {
            return new Payment
            {
                Id = dto.Id,
                PassengerId = dto.PassengerId,
                BookingId = dto.BookingId,
                Amount  = dto.Amount,
                PaymentDate = dto.PaymentDate,
                Status = dto.Status,
            };
        }


        public static PaymentDto ToDto(Payment payment)
        {
            return new PaymentDto
            {
                Id = payment.Id,
                PassengerId = payment.PassengerId,
                BookingId = payment.BookingId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                Status = payment.Status,
            };
        }
    }

}
