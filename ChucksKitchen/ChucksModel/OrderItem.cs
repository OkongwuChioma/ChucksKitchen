namespace ChucksKitchen.ChucksModel
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal PriceAtOrder { get; set; }
        public decimal SubTotal { get; set; }
    }
}

