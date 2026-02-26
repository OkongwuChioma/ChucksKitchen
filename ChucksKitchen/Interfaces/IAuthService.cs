using ChucksKitchen.Dtos.Request;
using ChucksKitchen.Dtos.Response;

namespace ChucksKitchen.Interfaces
{
    public interface IAuthService
    {
        Task<SignUpResponse> RegisterUserAsync(SignUpRequest request);
        Task<bool> VerifyOtpAsync(VerifyOtpRequestDto request);
        Task<bool> IsDuplicateUserAsync(string email, string phone);
    }
}
