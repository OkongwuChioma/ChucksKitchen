namespace ChucksKitchen.Dtos.Response
{
    public class FooditemResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}