namespace ChucksKitchen.Dtos.Response
{
    public class OrderStatusResponseDto
    {
        public int OrderId { get; set; }
        public string CurrentStatus { get; set; } = string.Empty;
        public List<StatusHistoryDto> StatusHistory { get; set; } = new();
        public DateTime LastUpdated { get; set; }
    }
}
