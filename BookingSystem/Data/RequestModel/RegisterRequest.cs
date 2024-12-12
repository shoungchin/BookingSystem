namespace BookingSystem.Data.RequestModel
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
