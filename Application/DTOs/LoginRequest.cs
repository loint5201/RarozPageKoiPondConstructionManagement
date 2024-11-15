using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string? Email { get; set; } = null!;

        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải ít nhất 6 ký tự")]
        public string? Password { get; set; } = null!;

        public bool RememberMe { get; set; } = false;
    }
}
