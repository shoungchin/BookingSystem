namespace BookingSystem.Data.CoreModel
{
    public class Package
    {
        public int PackageId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int Credits { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
