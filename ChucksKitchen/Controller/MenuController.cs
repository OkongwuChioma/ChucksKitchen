using ChucksKitchen.Dtos.Request;
using ChucksKitchen.Dtos.Response;
using ChucksKitchen.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChucksKitchen.Controller
{
    [Route("api/Menu")]
    [ApiController]

    public class Menucontroller(IFoodService foodService) : ControllerBase
    {
        private readonly IFoodService _foodservice = foodService;
        [HttpGet("Item")]
        [ProducesResponseType(typeof(ApiResponseDto<IEnumerable<FooditemResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAvailbleFood()
        {
            var foods = await _foodservice.GetAvailableFoodsAsync();
            return Ok(ApiResponseDto<IEnumerable<FooditemResponseDto>>.SuccessMessage(foods));
        }
        [HttpPost("Item")]
        [ProducesResponseType(typeof(ApiResponseDto<FooditemResponseDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddFood([FromBody] CreateFoodRequestDto request)
        {
            var result = await _foodservice.AddFoodAsync(request);
            return CreatedAtAction(nameof(GetAvailbleFood),
                ApiResponseDto<object>.SuccessMessage(request, "Food Item added"));
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponseDto<object>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var result = await _foodservice.DeleteFoodAsync(id);
            return Ok(ApiResponseDto<object>.SuccessMessage("item Deleted Successfully"));
        }


    }
}


