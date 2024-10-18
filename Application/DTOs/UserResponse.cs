namespace Application.DTOs
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string? FullName { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int? RoleId { get; set; }
    }
}
