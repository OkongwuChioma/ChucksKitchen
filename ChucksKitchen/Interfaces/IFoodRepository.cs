using ChucksKitchen.ChucksModel;

namespace ChucksKitchen.Interfaces
{
    public interface IFoodRepository
    {
        Task<IEnumerable<FoodItem>> GetAllAvailableFoodsAsync();
        Task<FoodItem?> GetFoodByIdAsync(int id);
        Task<FoodItem> CreateFoodAsync(FoodItem food);
        Task<bool> UpdateFoodAsync(FoodItem food);
        Task<bool> DeleteFoodAsync(int id);
    }
}
