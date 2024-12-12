using BookingSystem.Data.CoreModel;
using BookingSystem.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Services
{
    public class PackageRepository : IPackageRepository
    {
        private readonly BookingDbContext _context;

        public PackageRepository(BookingDbContext context)
        {
            _context = context;
        }

        public async Task<List<Package>> GetAvailablePackagesByCountryAsync(string country)
        {
            return await _context.Packages
                .Where(p => p.Country == country)
                .ToListAsync();
        }

        public async Task<List<UserPackage>> GetUserPackagesAsync(int userId)
        {
            return await _context.UserPackages
                .Include(up => up.Package)
                .Where(up => up.UserId == userId)
                .Select(up => new UserPackage
                {
                    UserPackageId = up.UserPackageId,
                    PackageId = up.PackageId,
                    UserId = up.UserId,
                    RemainingCredits = up.RemainingCredits,
                    IsExpired = up.Package.ExpiryDate < DateTime.UtcNow,
                    PurchasedDate = up.PurchasedDate,
                    Package = up.Package
                })
                .ToListAsync();
        }
    }

}
