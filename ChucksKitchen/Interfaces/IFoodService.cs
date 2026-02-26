using ChucksKitchen.Dtos.Request;
using ChucksKitchen.Dtos.Response;

namespace ChucksKitchen.Interfaces
{
    public interface IFoodService
    {
        Task<IEnumerable<FooditemResponseDto>> GetAvailableFoodsAsync();
        Task<FooditemResponseDto> AddFoodAsync(CreateFoodRequestDto request);
    }
}
