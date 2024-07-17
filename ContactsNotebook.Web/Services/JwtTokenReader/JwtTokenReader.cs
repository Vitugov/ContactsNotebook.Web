using Azure.Core;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ContactsNotebook.Web.Services.JwtTokenReader
{
    public static class JwtTokenReader
    {
        public static string GetRole(HttpRequest request)
        {
            var accessToken = request.Cookies["AccessToken"];
            if (accessToken == null)
            {
                return "";
            }
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(accessToken);

            var roleClaims = jwtToken.Claims.Where(claim => claim.Type == ClaimTypes.Role).ToList();
            //if (!roleClaims.Any())
            //{
            //    throw new ArgumentException("В токене отсутствуют данные о ролях пользователя");
            //}
            if (roleClaims.Any(role => role.Value == "Administrator"))
            {
                return "Administrator";
            }
            if (roleClaims.Any(role => role.Value == "User"))
            {
                return "User";
            }
            return "";
        }
    }
}
