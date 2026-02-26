namespace ChucksKitchen.ChucksModel
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Email { get; set; } =default!;
        public string PhoneNumber { get; set; } = default!;
        public string? ReferralCode { get; set; }
        public bool IsVerified { get; set; }
        public string Otp { get; set; }    = default!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

