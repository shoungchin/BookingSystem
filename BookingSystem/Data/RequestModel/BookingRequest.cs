namespace BookingSystem.Data.RequestModel
{
    public class BookingRequest
    {
        public int UserId { get; set; }      
        public int ScheduleId { get; set; }  
        public string PackageName { get; set; }
    }

}
