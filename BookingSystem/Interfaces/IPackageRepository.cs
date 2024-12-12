using BookingSystem.Data.CoreModel;

namespace BookingSystem.Interfaces
{
    public interface IPackageRepository
    {
        Task<List<Package>> GetAvailablePackagesByCountryAsync(string country);
        Task<List<UserPackage>> GetUserPackagesAsync(int userId);
    }
}
