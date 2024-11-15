using Application.Common.Security;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<UserResponse?> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || !PasswordUtil.VerifyPassword(password, user.PasswordHash))
                return null;

            return new UserResponse
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                RoleId = user.RoleId,
                Avatar = user.Avatar
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<(bool Success, string? ErrorMessage)> RegisterAsync(RegisterRequest request)
        {
            if (await _userRepository.AnyAsync(u => u.Email == request.Email || u.PhoneNumber == request.PhoneNumber))
                return (false, "Email hoặc số điện thoại đã tồn tại, vui lòng thử lại");

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = PasswordUtil.EncryptPassword(request.Password),
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                RoleId = (int)AppRoles.Customer
            };

            _userRepository.Add(user);
            await _userRepository.SaveChangesAsync();

            return (true, null);
        }
    }
}
