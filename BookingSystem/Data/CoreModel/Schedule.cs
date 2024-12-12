namespace BookingSystem.Data.CoreModel
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public string ClassName { get; set; }
        public string Country { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RequiredCredits { get; set; }
        public int MaxSlots { get; set; }
        public int AvailableSlots { get; set; }
    }
}
