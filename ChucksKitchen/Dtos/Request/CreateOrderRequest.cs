namespace ChucksKitchen.Dtos.Request
{
    public class CreateOrderRequest
    {
        public int Userid { get; set; }
        public List<CreateItem> Items { get; set; } = new();
        public string DeliveryAddress { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }
}
