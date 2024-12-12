namespace BookingSystem.Data.CoreModel
{
    public class Waitlist
    {
        public int WaitlistId { get; set; }
        public int UserId { get; set; }
        public int ScheduleId { get; set; }
        public DateTime WaitlistedAt { get; set; }
    }
}
