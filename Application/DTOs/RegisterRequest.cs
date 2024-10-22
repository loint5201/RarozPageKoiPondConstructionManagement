using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.DTOs
{
    public class RegisterRequest
    {
        public string? FullName { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? Password { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        [NotMapped]
        public IFormFile? Avatar { get; set; }
    }
}
