using ContactsNotebook.Lib.Services.JwtTokenHandler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace ContactsNotebook.Lib.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdministratorAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var jwtTokenHandler = context.HttpContext.RequestServices.GetRequiredService<JwtTokenHandler>();
            var token = jwtTokenHandler.GetTokenFromHeader(context);
            if (token == "")
            {
                context.Result = new ForbidResult();
            }

            var validatedToken = jwtTokenHandler.ValidateToken(token);
            if (validatedToken == null)
            {
                context.Result = new ForbidResult();
            }

            var isAdministrator = jwtTokenHandler.HasTokenRole(validatedToken!, "Administrator");
            if (!isAdministrator)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
