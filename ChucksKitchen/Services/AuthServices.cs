using ChucksKitchen.ChucksModel;
using ChucksKitchen.Dtos.Request;
using ChucksKitchen.Dtos.Response;
using ChucksKitchen.Interfaces;

namespace ChucksKitchen.Services
{
    public class AuthServices(IAppUser appUser) : IAuthService
    {
        private readonly IAppUser _appUser = appUser;
        public async Task<bool> IsDuplicateUserAsync(string email, string phone)
        {
            var user = await _appUser.GetUserByEmailOrPhoneAsync(email, phone);
            return user != null;
        }

        public async Task<SignUpResponse> RegisterUserAsync(SignUpRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) && string.IsNullOrWhiteSpace(request.PhoneNumber))
                throw new ArgumentException("PhoneNumber and Email are required");
            if (await IsDuplicateUserAsync(request.Email ?? string.Empty, request.PhoneNumber ?? string.Empty))
                throw new ArgumentException("PhoneNumber and Email already exist");
            var user = new AppUser
            {
                Email = request.Email ?? string.Empty,
                PhoneNumber = request.PhoneNumber??string.Empty,
                ReferralCode=request.ReferralCode
            };
            var createdUser = await _appUser.CreateUserAsync(user);
            return new SignUpResponse
            {
                Message = "User created successfully",
                UserId = createdUser.Id,
                RegisteredAt = createdUser.CreatedAt,
                SimulatedOtp = createdUser.Otp
            };
        }

        public Task<bool> VerifyOtpAsync(VerifyOtpRequestDto request)
        {
            var user=_appUser.VerifyUserOtpAsync(request.UserId, request.Otp);
            return Task.FromResult(true);
        }
    }
}
