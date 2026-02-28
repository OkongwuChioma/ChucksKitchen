using ChucksKitchen.ChucksModel;

namespace ChucksKitchen.Dtos.Response
{
    public class OrderResponseDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public List<OrderItem> Items { get; set; } = new();
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;
        public DateTime OrderedAt { get; set; }
        public string EstimatedDelivery { get; set; } = string.Empty;
    }
}

