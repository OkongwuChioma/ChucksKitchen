namespace ChucksKitchen.Dtos.Response
{
    public class SignUpResponse
    {
        public int UserId { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? SimulatedOtp { get; set; }
        public DateTime RegisteredAt { get; set; }
    }
}

