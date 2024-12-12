using BookingSystem.Data.CoreModel;
using System.Security.Claims;

namespace BookingSystem.Interfaces
{
    public interface IJwtService
    {
        public string GenerateToken(User user);
        bool ValidateToken(string token);
        IEnumerable<Claim> GetClaimsFromToken(string token);

    }
}
