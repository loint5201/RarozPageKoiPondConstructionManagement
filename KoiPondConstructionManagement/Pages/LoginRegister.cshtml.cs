using Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace KoiPondConstructionManagement.Pages
{
    public class LoginRegisterModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IValidator<LoginRequest> _loginRequestValidator;
        private readonly IValidator<RegisterRequest> _registerRequestValidator;

        public LoginRegisterModel(IUserService userService, IValidator<LoginRequest> loginRequestValidator, IValidator<RegisterRequest> registerRequestValidator)
        {
            _userService = userService;
            _loginRequestValidator = loginRequestValidator;
            _registerRequestValidator = registerRequestValidator;
        }

        [BindProperty]
        public LoginRequest LoginModel { get; set; }

        [BindProperty]
        public RegisterRequest RegisterModel { get; set; }

        public void OnGet()
        {
        }

        // Xử lý đăng nhập
        public async Task<IActionResult> OnPostLoginAsync()
        {

            var isValid = await _loginRequestValidator.ValidateAsync(LoginModel);
            if (!isValid.IsValid)
            {
                ModelState.AddModelError("LoginModel", isValid.ToString());
                return Page();
            }

            var user = await _userService.LoginAsync(LoginModel.Email, LoginModel.Password);
            if (user != null)
            {
                // Tạo ClaimsIdentity
                var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new(ClaimTypes.Name, user.FullName),
                    new(ClaimTypes.Email, user.Email),
                    new(ClaimTypes.Role, user.RoleId.ToString()),
                    new(ClaimTypes.MobilePhone, user.PhoneNumber),
                };
                var claimsIdentity = new ClaimsIdentity(claims, "login");
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = LoginModel.RememberMe, // Ghi nhớ người dùng nếu đã chọn
                    ExpiresUtc = LoginModel.RememberMe ? DateTimeOffset.UtcNow.AddDays(30) : (DateTimeOffset?)null // Thời gian hết hạn
                };

                //var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                // Đăng nhập người dùng
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);


                // Đăng nhập thành công, điều hướng tới trang khác
                return RedirectToPage("/Index");
            }
            ModelState.AddModelError("LoginModel", "Invalid login attempt.");
            return Page();
        }

        // Xử lý đăng ký
        public async Task<IActionResult> OnPostRegisterAsync()
        {
            var isValid = await _registerRequestValidator.ValidateAsync(RegisterModel);
            if (!isValid.IsValid)
            {
                ModelState.AddModelError("RegisterModel", isValid.ToString());
                return Page();
            }

            var result = await _userService.RegisterAsync(RegisterModel);
            if (result.Success)
            {
                ModelState.AddModelError("RegisterModel", "Register successfully.");
            }
            ModelState.AddModelError("RegisterModel", result.ErrorMessage);
            return Page();
        }
    }
}
