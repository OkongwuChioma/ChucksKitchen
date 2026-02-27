using ChucksKitchen.ChucksModel;
using ChucksKitchen.Dtos.Request;
using ChucksKitchen.Dtos.Response;
using ChucksKitchen.Interfaces;

namespace ChucksKitchen.Services
{
    public class FoodService(IFoodRepository foodRepo) : IFoodService
    {
        private readonly IFoodRepository _foodRepository = foodRepo;

        public async Task<FooditemResponseDto> AddFoodAsync(CreateFoodRequestDto request)
        {
            var food = new FoodItem
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                Category = request.Category,
                StockCount = request.StockCount,
                ImageUrl = request.ImageUrl
            };
            var createdFood = await _foodRepository.CreateFoodAsync(food);
            return new FooditemResponseDto
            {
                Name = createdFood.Name,
                Description = createdFood.Description,
                Category = createdFood.Category,
                ImageUrl = createdFood.ImageUrl,
                IsAvailable = createdFood.StockCount > 0,
                Price = createdFood.Price,
                Id = createdFood.Id
            };
        }

        public async Task<IEnumerable<FooditemResponseDto>> GetAvailableFoodsAsync()
        {
            var available = await _foodRepository.GetAllAvailableFoodsAsync();
            return available.Select(f => new FooditemResponseDto
            {
                Id = f.Id,
                Description = f.Description,
                Category = f.Category,
                ImageUrl = f.ImageUrl,
                IsAvailable = f.StockCount > 0,
                Name = f.Name,
                Price = f.Price
            });
        }
        public async Task<bool> DeleteFoodAsync(int id)
        {
            await _foodRepository.DeleteFoodAsync(id);
            return true;
        }

    }
}
