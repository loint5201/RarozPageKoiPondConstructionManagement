using Domain.Enums;
using Domain.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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
