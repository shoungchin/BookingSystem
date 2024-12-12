using BookingSystem.Data.CoreModel;
using BookingSystem.Data.RequestModel;
using BookingSystem.Data.ResponseModel;
using BookingSystem.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace BookingSystem.Services
{
    public class UserService : IUserService
    {
        private readonly BookingDbContext _context;
        private readonly IJwtService _jwtService;

        public UserService(BookingDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<ServiceResponse<string>> RegisterUserAsync(RegisterRequest request)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                return new ServiceResponse<string> { Success = false, Message = "Email already exists" };

            var user = new User
            {
                Email = request.Email,
                Password = HashPassword(request.Password),
                Name = request.Name,
                Country = request.Country,
                IsEmailVerified = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // Send verification email
            return new ServiceResponse<string> { Success = true, Message = "User registered successfully" };
        }

        public async Task<ServiceResponse<string>> LoginUserAsync(LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || !VerifyPassword(user.Password, request.Password))
                return new ServiceResponse<string> { Success = false, Message = "Invalid credentials" };

            var token = _jwtService.GenerateToken(user);
            return new ServiceResponse<string> { Success = true, Token = token };
        }

        public async Task<User> GetUserProfileAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<ServiceResponse<string>> ChangePasswordAsync(int userId, ChangePasswordRequest request)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || !VerifyPassword(user.Password, request.CurrentPassword))
                return new ServiceResponse<string> { Success = false, Message = "Current password is incorrect" };

            user.Password = HashPassword(request.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return new ServiceResponse<string> { Success = true, Message = "Password changed successfully" };
        }

        public async Task<ServiceResponse<string>> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                return new ServiceResponse<string> { Success = false, Message = "Email not found" };

            user.Password = HashPassword(request.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return new ServiceResponse<string> { Success = true, Message = "Password reset successfully" };
        }
        private string HashPassword(string password)
        {
            // Generate a salt
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Hash the password with the salt
            byte[] hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);

            // Combine salt and hash into one array
            byte[] saltAndHash = new byte[salt.Length + hash.Length];
            Array.Copy(salt, 0, saltAndHash, 0, salt.Length);
            Array.Copy(hash, 0, saltAndHash, salt.Length, hash.Length);

            // Convert the combined salt and hash to a Base64 string for storage
            return Convert.ToBase64String(saltAndHash);
        }

        private bool VerifyPassword(string storedSaltAndHash, string password)
        {
            // Decode the stored salt and hash
            byte[] saltAndHashBytes = Convert.FromBase64String(storedSaltAndHash);

            // Extract the salt (first 16 bytes)
            byte[] salt = new byte[16];
            Array.Copy(saltAndHashBytes, 0, salt, 0, 16);

            // Extract the hash (remaining bytes)
            byte[] storedHash = new byte[saltAndHashBytes.Length - 16];
            Array.Copy(saltAndHashBytes, 16, storedHash, 0, storedHash.Length);

            // Hash the input password using the extracted salt
            byte[] hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);

            // Compare the computed hash with the stored hash
            return hash.SequenceEqual(storedHash);
        }

    }
}
