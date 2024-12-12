using BookingSystem.Data.CoreModel;
using BookingSystem.Data.RequestModel;
using BookingSystem.Data.ResponseModel;

namespace BookingSystem.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<string>> RegisterUserAsync(RegisterRequest request);
        Task<ServiceResponse<string>> LoginUserAsync(LoginRequest request);
        Task<User> GetUserProfileAsync(int userId);
        Task<ServiceResponse<string>> ChangePasswordAsync(int userId, ChangePasswordRequest request);
        Task<ServiceResponse<string>> ResetPasswordAsync(ResetPasswordRequest request);

    }
}
