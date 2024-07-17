using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace ContactsNotebook.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdministratorAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isAdministrator = context.HttpContext.User.HasClaim(ClaimTypes.Role, "Administrator");
            if (!isAdministrator)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
