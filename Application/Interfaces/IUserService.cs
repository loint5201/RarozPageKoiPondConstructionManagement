using Application.DTOs;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse?> LoginAsync(string email, string password);
        Task<(bool Success, string? ErrorMessage)> RegisterAsync(RegisterRequest request);
    }
}
