using ChucksKitchen.ChucksModel;

namespace ChucksKitchen.Interfaces
{
    public interface IOrder
    {
        Task<Order> CreateOrderAsync(Order order);
        Task<Order?> GetOrderByIdAsync(int id);
        Task<Order?> GetOrderWithItemsAsync(int id);
        Task<bool> UpdateOrderStatusAsync(int orderId, string newStatus);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
    }
}
