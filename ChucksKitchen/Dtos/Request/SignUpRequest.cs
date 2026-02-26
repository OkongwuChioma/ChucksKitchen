namespace ChucksKitchen.Dtos.Request
{
    public class SignUpRequest
    {
        public string? Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? ReferralCode { get; set; } = string.Empty;
    }
}    
