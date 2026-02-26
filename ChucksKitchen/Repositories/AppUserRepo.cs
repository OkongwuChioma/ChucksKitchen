using ChucksKitchen.ChucksModel;
using ChucksKitchen.Constants;
using ChucksKitchen.Data;
using ChucksKitchen.Interfaces;

namespace ChucksKitchen.Repositories
{
    public class AppUserRepo : BaseRepo, IAppUser
    {
        private readonly DataStorage _dataStorage;
        public AppUserRepo(DataStorage dataStorage) : base(dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public Task<AppUser> CreateUserAsync(AppUser user)
        {
            var userFile = GetDataFile<AppUser>(DbTableNames.Users);

            var existingUser = userFile.FirstOrDefault(u => u.Email == user.Email);
            if (existingUser != null) return Task.FromResult(existingUser);

            user.Id = userFile.Count + 1;
            user.Otp = new Random().Next(1000, 9999).ToString();
            userFile.Add(user);
            _dataStorage.DbFile[DbTableNames.Users] = userFile;
            return Task.FromResult(user);
        }

        public Task<AppUser?> GetUserByEmailOrPhoneAsync(string email, string phone)
        {
            var user = GetDataFile<AppUser>(DbTableNames.Users);
            return Task.FromResult(user.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) ||
            u.PhoneNumber == phone));
        }

        public Task<AppUser?> GetUserByIdAsync(int id)
        {
            var user = GetDataFile<AppUser>(DbTableNames.Users);
            var existingUser = user.FirstOrDefault(u => u.Id == id);
            return Task.FromResult(existingUser);
        }

        public Task<List<AppUser>> GetUsers()
        {
            var user = GetDataFile<AppUser>(DbTableNames.Users);
            return Task.FromResult(user.ToList());
        }

        public Task<bool> UpdateUserAsync(AppUser user)
        {
            var userFile = GetDataFile<AppUser>(DbTableNames.Users);
            var existingUser = userFile.FirstOrDefault(u => u.Id == user
            .Id);
            if (existingUser == null)
            {
                return Task.FromResult(false);
            }
            existingUser.Email = user.Email;
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.IsVerified = user.IsVerified;

            return Task.FromResult(true);
        }

        public Task<bool> VerifyUserOtpAsync(int userId, string otp)
        {
            var userFile = GetDataFile<AppUser>(DbTableNames.Users);
            var user = userFile.FirstOrDefault(u => u.Id == userId && u.Otp == otp);
            if (user == null || user.Otp != otp)
            {
                return Task.FromResult(false);
            }
            user.IsVerified = true;
            user.Otp = string.Empty; // Clear OTP after successful verification
            return Task.FromResult(true);
        }
    }
}