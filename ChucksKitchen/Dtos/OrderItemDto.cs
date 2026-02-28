namespace ChucksKitchen.Dtos
{
    public class OrderItemDto
    {
        public string FoodName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal PriceAtOrder { get; set; }
        public decimal SubTotal { get; set; }
    }
}
