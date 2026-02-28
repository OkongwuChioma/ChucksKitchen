using ChucksKitchen.Dtos.Request;
using ChucksKitchen.Dtos.Response;
using ChucksKitchen.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChucksKitchen.Controller
{
    [Route("api/Order")]
    [ApiController]
    public class OrderController(IOrderService orderService,
        ILogger<OrderController> logger) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;
        private readonly ILogger _logger = logger;
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseDto<OrderResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrderbyId([FromRoute] int id)
        {
            try
            {
                var orderId = await _orderService.GetOrderByIdAsync(id);
                return Ok(ApiResponseDto<OrderResponseDto>.SuccessMessage(orderId));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponseDto<object>.ErrorMessage(ex.Message));
            }
        }
        [HttpPost("Order")]
        [ProducesResponseType(typeof(ApiResponseDto<OrderResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponseDto<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            try
            {
                var order = await _orderService.CreateOrderAsync(request);
                return CreatedAtAction(nameof(GetOrderbyId), new { id = order.OrderId },
                    ApiResponseDto<OrderResponseDto>.SuccessMessage(order, "Order Created sucessfully"));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponseDto<object>.ErrorMessage(ex.Message));
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Order validation failed");
                return BadRequest(ApiResponseDto<object>.ErrorMessage(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order");
                return StatusCode(500, ApiResponseDto<object>.ErrorMessage("Order already Exist"));
            }
        }
        [HttpPatch("{id}/Cancel")]
        public async Task<IActionResult> CancelOrder([FromRoute] int id, CancelOrderRequestDto request)
        {
            var success = await _orderService.CancelOrderAsync(id, request.UserId);
            return success
                ? Ok(ApiResponseDto<object>.SuccessMessage(null, "Order Cancelled"))
                : BadRequest(ApiResponseDto<object>.ErrorMessage("cannot cancel order"));
        }
    }
}
