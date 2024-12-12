namespace BookingSystem.Data.CoreModel
{
    public class UserPackage
    {
        public int UserPackageId { get; set; }
        public int UserId { get; set; }
        public int PackageId { get; set; }
        public int RemainingCredits { get; set; }
        public bool IsExpired { get; set; }
        public DateTime PurchasedDate { get; set; }

        public Package Package { get; set; } 
    }
}
