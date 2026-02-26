namespace ChucksKitchen.Dtos.Request
{
    public class VerifyOtpRequestDto
    {
        public int UserId { get; set; }
        public string Otp { get; set; } = string.Empty;
    }
}
