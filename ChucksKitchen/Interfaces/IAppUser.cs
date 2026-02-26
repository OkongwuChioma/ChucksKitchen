using ChucksKitchen.ChucksModel;
using ChucksKitchen.Dtos.Request;

namespace ChucksKitchen.Interfaces
{
    public interface IAppUser
    {
       
        Task<List<AppUser>> GetUsers();
        Task<AppUser?> GetUserByIdAsync(int id);
        Task<AppUser?> GetUserByEmailOrPhoneAsync(string email, string phone);
        Task<AppUser> CreateUserAsync(AppUser user);
        Task<bool> UpdateUserAsync(AppUser user);
        Task<bool> VerifyUserOtpAsync(int userId, string otp);
    }
}
