
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Coinbase.Services.Identity.Entities;

namespace Coinbase.Services.Identity.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<Role> _roles;

        public AuthorizeAttribute(params Role[] roles)
        {
            _roles = roles ?? Array.Empty<Role>();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Skip authorization if action is decorated with [AllowAnonymous] attribute
            bool allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
            {
                return;
            }

            // Authorization
            if (context.HttpContext.Items["Owner"] is not Owner user || (_roles.Any() && !_roles.Contains(user.Role)))
            {
                // Not logged in or role not authorized
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
