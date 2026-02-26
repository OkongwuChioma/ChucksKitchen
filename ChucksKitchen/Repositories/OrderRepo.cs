using ChucksKitchen.ChucksModel;
using ChucksKitchen.Constants;
using ChucksKitchen.Data;
using ChucksKitchen.Interfaces;

namespace ChucksKitchen.Repositories
{
    public class OrderRepo : BaseRepo, IOrder
    {
        private readonly DataStorage _dataStorage;
        public OrderRepo(DataStorage dataStorage) : base(dataStorage)
        {

        }
        public Task<Order> CreateOrderAsync(Order order)
        {
            var orderFile = GetDataFile<Order>(DbTableNames.Orders);
            var existing = orderFile.FirstOrDefault(o => o.Id == order.Id);
            if (existing != null) return Task.FromResult(existing);
            order.CreatedAt = DateTime.Now;
            orderFile.Add(order);
            return Task.FromResult(order);
        }

        public Task<Order?> GetOrderByIdAsync(int id)
        {
            var orderFile = GetDataFile<Order>(DbTableNames.Orders);
            return Task.FromResult(orderFile.FirstOrDefault(o => o.Id == id));
        }

        public Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            var orderFile = GetDataFile<Order>(DbTableNames.Orders);
            return Task.FromResult(orderFile.Where(o => o.Id == userId).AsEnumerable());
        }

        public Task<Order?> GetOrderWithItemsAsync(int id)
        {
            var orderFile = GetDataFile<Order>(DbTableNames.Orders);
            return Task.FromResult(orderFile.FirstOrDefault(o => o.Id == id));
        }

        public Task<bool> UpdateOrderStatusAsync(int orderId, string newStatus)
        {
            var orderFile = GetDataFile<Order>(DbTableNames.Orders);
            var existing = orderFile.FirstOrDefault(O => O.Id == orderId);
            if (existing == null) return Task.FromResult(false);
            existing.Status = newStatus;
            existing.UpdatedAt = DateTime.Now;
            return Task.FromResult(true);
        }
    }
}
