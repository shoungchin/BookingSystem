using BookingSystem.Data.CoreModel;

namespace BookingSystem.Interfaces
{
    public interface IPackageService
    {
        Task<List<Package>> GetAvailablePackagesAsync(string country);
        Task<List<UserPackage>> GetUserPackagesAsync(int userId);
    }
}
