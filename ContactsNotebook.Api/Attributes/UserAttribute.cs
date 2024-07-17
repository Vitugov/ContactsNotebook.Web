using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ContactsNotebook.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UserAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isUser = context.HttpContext.User.HasClaim(ClaimTypes.Role, "User");
            if (!isUser)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
