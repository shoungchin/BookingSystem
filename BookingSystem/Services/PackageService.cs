using BookingSystem.Data.CoreModel;
using BookingSystem.Interfaces;

namespace BookingSystem.Services
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _packageRepository;

        public PackageService(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public async Task<List<Package>> GetAvailablePackagesAsync(string country)
        {
            return await _packageRepository.GetAvailablePackagesByCountryAsync(country);
        }

        public async Task<List<UserPackage>> GetUserPackagesAsync(int userId)
        {
            return await _packageRepository.GetUserPackagesAsync(userId);
        }
    }

}
