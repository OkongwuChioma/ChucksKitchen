using ChucksKitchen.ChucksModel;
using ChucksKitchen.Dtos;
using ChucksKitchen.Dtos.Request;
using ChucksKitchen.Dtos.Response;
using ChucksKitchen.Interfaces;

namespace ChucksKitchen.Services
{
    public class OrderService(IOrder orderRepo, IAppUser appUser, IFoodRepository foodRepo) : IOrderService
    {
        private readonly IOrder _orderRepo = orderRepo;
        private readonly IAppUser _appUser = appUser;
        private readonly IFoodRepository _foodRepo = foodRepo;
        public async Task<bool> CancelOrderAsync(int userId, int orderId)
        {
            var order = await _orderRepo.GetOrderByIdAsync(orderId) ?? throw new ArgumentException($"Order with Id: {orderId} doesnt Exist");
            if (order.UserId != userId) throw new ArgumentException($"Order doesnt belong to the user");
            if (order.Status != "Pending") return true;
            return await _orderRepo.UpdateOrderStatusAsync(orderId, "order cancelled");
        }

        public async Task<OrderResponseDto> CreateOrderAsync(CreateOrderRequest request)
        {
            var user = await _appUser.GetUserByIdAsync(request.Userid) ?? throw new
                ArgumentException($"user with Id:{request.Userid} does not exist");
            var orderItems = new List<OrderItem>();
            var foodMenu = new List<FoodItem>();

            foreach (var item in request.Items)//item is each item in the cart
            {
                var food = await _foodRepo.GetFoodByIdAsync(item.FoodId);
                if (food == null)
                    continue;

                if (item.Quantity > food.StockCount) throw new Exception($"{food.StockCount} quantity of {item.FoodId} is available");

                food.StockCount -= item.Quantity;
                foodMenu.Add(food);
                orderItems.Add(new OrderItem
                {
                    FoodId = food.Id,
                    PriceAtOrder = food.Price,
                    Quantity = item.Quantity,
                    Amount = food.Price
                });
            }

            foreach (var foodItem in foodMenu)
                await _foodRepo.UpdateFoodAsync(foodItem);

            decimal total = 0;
            orderItems.ForEach(e => total += e.Amount * e.Quantity);
            var order = new Order
            {
                UserId = request.Userid,
                Items = orderItems,
                Status = "pending",
                DeliveryAddress = request.DeliveryAddress,
                Note = request.Notes,
                TotalAmount = total,
                CreatedAt = DateTime.Now
            };

            await _orderRepo.CreateOrderAsync(order);
            return MapToResponseDto(order);
        }

        public async Task<OrderResponseDto?> GetOrderByIdAsync(int id)
        {

            var order = await _orderRepo.GetOrderByIdAsync(id);
            if (order == null) throw new Exception("order doesnt exist yet");
            var item = new List<OrderItem>();
            return new OrderResponseDto
            {
                OrderId = order.Id,
                UserId = order.UserId,
                Items = order.Items.Select(o => new OrderItem
                {

                    FoodName = o.FoodName,
                    PriceAtOrder = o.PriceAtOrder,
                    Amount = o.Amount,
                    Quantity = o.Quantity,
                    SubTotal = o.SubTotal
                }).ToList(),
                DeliveryAddress = order.DeliveryAddress,
                OrderedAt = order.CreatedAt,
                TotalAmount = order.TotalAmount,
            };
        }

        public async Task<OrderStatusResponseDto> GetOrderStatusAsync(int id)
        {
            var order = await _orderRepo.GetOrderByIdAsync(id);
            if (order == null)
                throw new ArgumentException("orderId doesnt exist");
            var orderStatus = new OrderStatusResponseDto
            {
                OrderId = order.Id,
                StatusHistory = new List<StatusHistoryDto>
                {
                   new() {Status=order.Status,Note=order.Note,Timestamp=order.UpdatedAt}
                },
                CurrentStatus = order.Status,
                LastUpdated = order.UpdatedAt
            };
            return orderStatus;
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _orderRepo.GetOrderByIdAsync(orderId);
            if (order == null) throw new KeyNotFoundException($"order with id: {orderId} does not exist");
            return await UpdateOrderStatusAsync(orderId, status);
        }
        private OrderResponseDto MapToResponseDto(Order order)
        {
            return new OrderResponseDto
            {
                OrderId = order.Id,
                UserId = order.UserId,
                Items = order.Items.Select(item => new OrderItem
                {
                    FoodId = item.FoodId,
                    FoodName = "Food#" + item.FoodId,
                    Quantity = item.Quantity,
                    SubTotal = item.SubTotal,
                    PriceAtOrder = item.PriceAtOrder
                }).ToList(),
                Status = order.Status,
                DeliveryAddress = order.DeliveryAddress,
                EstimatedDelivery = order.CreatedAt.AddMinutes(30).ToString("hh:mm")
            };
        }
    }
}
