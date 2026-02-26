namespace ChucksKitchen.Dtos
{
    public class StatusHistoryDto
    {
        public string Status { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string? Note { get; set; }
    }
}
