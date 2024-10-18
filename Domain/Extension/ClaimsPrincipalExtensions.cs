using Domain.Enums;
using System.Security.Claims;

namespace Domain.Extension
{
    public static class ClaimsPrincipalExtensions
    {
        public static int? GetUserId(this ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userId, out int id))
            {
                return id;
            }

            return 0;
        }

        public static AppRoles GetRole(this ClaimsPrincipal user)
        {
            var roleId = user.FindFirst(ClaimTypes.Role)?.Value;
            if (int.TryParse(roleId, out int id))
            {
                return (AppRoles)id;
            }

            return (AppRoles)1;
        }
    }
}
