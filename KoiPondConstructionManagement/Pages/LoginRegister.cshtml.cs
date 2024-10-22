﻿using Application.Interfaces;
using Domain.Enums;
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
                    new("Avatar", user.Avatar ?? ""),
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

                return user.RoleId switch
                {
                    (int)AppRoles.Manager => RedirectToPage("/Manager/Admin/Index"),
                    (int)AppRoles.Customer => RedirectToPage("/Index"),
                    (int)AppRoles.Construction_Staff => RedirectToPage("/Manager/Construction/Index"),
                    (int)AppRoles.Design_Staff => RedirectToPage("/Manager/Design/Index"),
                    (int)AppRoles.Consulting_Staff => RedirectToPage("/Manager/Consulting/Index"),
                    _ => RedirectToPage("/Index"),
                };

            }
            ModelState.AddModelError("LoginModel", "Sai tên đăng nhập hoặc mật khẩu.");
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
                ModelState.AddModelError("RegisterModel", "Đăng ký thành công.");
            }
            else
            {
                ModelState.AddModelError("RegisterModel", result.ErrorMessage ?? "");
            }
            return Page();
        }
    }
}
