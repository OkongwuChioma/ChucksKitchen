namespace ChucksKitchen.ChucksModel
{
    public class FoodItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category { get; set; } = default!;
        public int StockCount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
