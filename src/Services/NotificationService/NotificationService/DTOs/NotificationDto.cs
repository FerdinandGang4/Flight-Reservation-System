namespace NotificationService.DTOs
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public int PassengerId { get; set; }
        public int BookingId { get; set; }
        public string Message { get; set; }
        public DateTime NotificationDate { get; set; }
    }
}
