namespace ChucksKitchen.ChucksModel
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<OrderItem> Items { get; set; }  = new List<OrderItem>();
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "pending"; // Pending, Confirmed, etc.
        public string DeliveryAddress { get; set; } = default!;
        public string? Note { get; set; } = default!;
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; } =DateTime.Now;
        public AppUser? User { get; set; }// Navigation property for future DB expansion
    }
}


