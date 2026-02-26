namespace ChucksKitchen.Dtos.Response
{
    public class ApiResponseDto<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new();
        public int StatusCode { get; set; }
        public static ApiResponseDto<T> SuccessMessage(T data, string message = "successfull well done") =>
             new() { Success = true, Message = message, Data = data, StatusCode=200 };
        public static ApiResponseDto<T> ErrorMessage(string message = "Not successfull", List<string> errors = null) => new()
        { Message = message, Success = false, StatusCode = 400, Errors = errors ?? new() };
    }
}
