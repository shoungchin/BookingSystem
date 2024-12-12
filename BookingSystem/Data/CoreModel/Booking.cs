namespace BookingSystem.Data.CoreModel
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int ScheduleId { get; set; }
        public string Status { get; set; } 
        public DateTime BookedAt { get; set; }
        public DateTime? CancelledAt { get; set; }
        public string CheckInStatus { get; set; }
    }
}
