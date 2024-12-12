namespace BookingSystem.Data.RequestModel
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }

    }
}
