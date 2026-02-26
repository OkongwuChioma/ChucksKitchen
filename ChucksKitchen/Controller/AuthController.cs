using ChucksKitchen.Dtos.Request;
using ChucksKitchen.Dtos.Response;
using ChucksKitchen.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChucksKitchen.Controller
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController(IAuthService authService,ILogger<AuthController> logger) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly ILogger<AuthController> _logger = logger;

        [HttpPost("signup")]
        [ProducesResponseType(typeof(ApiResponseDto<SignUpResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponseDto<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponseDto<object>), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> SignUp([FromBody]SignUpRequest request)
        {
            try
            {
                var result = await _authService.RegisterUserAsync(request);
                return Ok(CreatedAtAction(nameof(SignUp),
                    ApiResponseDto<SignUpResponse>.SuccessMessage(result, "Registration successful")));
            }
            catch (ArgumentException exception)
            {
                _logger.LogWarning(exception, "Validation error during signup");
                return BadRequest(ApiResponseDto<object>.ErrorMessage(exception.Message));
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex,"duplicate user attempt");
                return BadRequest(ApiResponseDto<object>.ErrorMessage(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex,"Unexpected error occured");
                return BadRequest(ApiResponseDto<object>.ErrorMessage("Unexpected error occured"));

            }
        }
        [HttpPost("verify")]
        public async Task<IActionResult>   VerifyOtp([FromBody]VerifyOtpRequestDto request)
        {
            var otp=await _authService.VerifyOtpAsync(request);
            return otp ? Ok(ApiResponseDto<object>.SuccessMessage(null, "account verified"))
                : BadRequest(ApiResponseDto<object>.ErrorMessage("invalid or expired otp"));
        }


    }
}

