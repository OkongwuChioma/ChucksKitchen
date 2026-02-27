namespace ChucksKitchen.Dtos.Response
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
    }
}
