using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Domain.Enums;
using Domain.Extension;

namespace KoiPondConstructionManagement.Middleware
{
    [AttributeUsage(AttributeTargets.All)]
    public class AuthorizeRoleAttribute(params AppRoles[] roles) : Attribute, IAuthorizationFilter
    {
        private readonly AppRoles[] _roles = roles;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var role = context.HttpContext.User.GetRole();
            if (!_roles.Contains(role))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
