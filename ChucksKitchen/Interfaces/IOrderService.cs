using ChucksKitchen.Dtos.Request;
using ChucksKitchen.Dtos.Response;

namespace ChucksKitchen.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDto> CreateOrderAsync(CreateOrderRequest request);
        Task<OrderStatusResponseDto?> GetOrderStatusAsync(int id);
        Task<bool> CancelOrderAsync(int userId, int orderId);
        Task<bool> UpdateOrderStatusAsync(int orderId, string status);
    }
}
