using Domain.Entities;
using Domain.Enums;
using System.Security.Claims;

namespace Domain.Extension
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int? GetUserId(this ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userId, out int id))
            {
                return id;
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static AppRoles GetRole(this ClaimsPrincipal user)
        {
            var roleId = user.FindFirst(ClaimTypes.Role)?.Value;
            if (int.TryParse(roleId, out int id))
            {
                return (AppRoles)id;
            }

            return (AppRoles)1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool IsCustomer(this ClaimsPrincipal user) => user.GetRole() == AppRoles.Customer;

        public static bool IsRole(this ClaimsPrincipal user, AppRoles role) => user.GetRole() == role;

        public static User? GetUser(this ClaimsPrincipal user)
        {
            if (user == null)
            {
                return null;
            }

            return new User
            {
                UserId = user.GetUserId().Value,
                FullName = user.FindFirst(ClaimTypes.Name)?.Value,
                Email = user.FindFirst(ClaimTypes.Email)?.Value,
                RoleId = (int)user.GetRole(),
                PhoneNumber = user.FindFirst(ClaimTypes.MobilePhone)?.Value,
                Avatar = user.FindFirst("Avatar")?.Value
            };
        }
    }
}
