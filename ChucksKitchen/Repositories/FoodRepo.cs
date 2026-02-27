using ChucksKitchen.ChucksModel;
using ChucksKitchen.Constants;
using ChucksKitchen.Data;
using ChucksKitchen.Interfaces;

namespace ChucksKitchen.Repositories
{
    public class FoodRepo : BaseRepo, IFoodRepository
    {
        private readonly DataStorage _dataStorage;
        public FoodRepo(DataStorage dataStorage) : base(dataStorage)
        {
            _dataStorage = dataStorage;
        }
        public Task<FoodItem> CreateFoodAsync(FoodItem food)
        {
            EnsureDefault();
            var menuList = GetDataFile<FoodItem>(DbTableNames.Menu);
            var existingFood = menuList.FirstOrDefault(f => f.Name.Equals
            (food.Name, StringComparison.OrdinalIgnoreCase));
            if (existingFood != null) return Task.FromResult(existingFood);

            food.Id = menuList.Count + 1;
            menuList.Add(food);

            _dataStorage.DbFile[DbTableNames.Menu] = menuList;
            return Task.FromResult(food);
        }

        public Task<bool> DeleteFoodAsync(int id)
        {
            var menuList = GetDataFile<FoodItem>(DbTableNames.Menu);
            var check = menuList.FirstOrDefault(s => s.Id == id);
            if (check == null) return Task.FromResult(false);
            menuList.Remove(check);
            return Task.FromResult(true);
        }

        public async Task<IEnumerable<FoodItem>> GetAllAvailableFoodsAsync()
        {
            EnsureDefault();
            var menus = GetDataFile<FoodItem>(DbTableNames.Menu);
            if (!menus.Any(f => f.Id == 1)) menus.Add(new FoodItem { Id = 1, Name = "Burger", Price = 5.99m, StockCount = 50 });
            if (!menus.Any(f => f.Id == 2)) menus.Add(new FoodItem { Id = 2, Name = "Pizza", Price = 8.99m, StockCount = 30 });
            var items = menus.Where(f => f.StockCount > 0);
            return await Task.FromResult(items);
        }

        public async Task<FoodItem?> GetFoodByIdAsync(int id)
        {
            var foodFile = GetDataFile<FoodItem>(DbTableNames.Menu);
            FoodItem? food = null;
            await Task.Run(() => { food = foodFile.FirstOrDefault(f => f.Id == id); });
            return food;
        }

        public async Task<bool> UpdateFoodAsync(FoodItem food)
        {
            var foodFile = GetDataFile<FoodItem>(DbTableNames.Menu);
            var existingFood = foodFile.FirstOrDefault(f => f.Id == food.Id);
            if (existingFood == null) return await Task.FromResult(false);

            existingFood.Name = food.Name;
            existingFood.Description = food.Description;
            existingFood.Price = food.Price;
            existingFood.StockCount = food.StockCount;
            return await Task.FromResult(true);
        }
        private void EnsureDefault()
        {
            var menus = GetDataFile<FoodItem>(DbTableNames.Menu);
            if (!menus.Any())
            {
                menus.Add(new FoodItem { Id = 1, Name = "Burger", Price = 5.99m, StockCount = 50 });
                menus.Add(new FoodItem { Id = 2, Name = "Pizza", Price = 8.99m, StockCount = 30 });

                _dataStorage.DbFile[DbTableNames.Menu] = menus;
            }
        }

    }
}
