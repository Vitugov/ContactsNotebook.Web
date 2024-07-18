using ContactsNotebook.Lib.Models.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ContactsNotebook.Lib.Services.JwtTokenHandler
{
    public class JwtTokenHandler
    {
        public readonly TokenValidationParameters TokenValidationParameters;

        public JwtTokenHandler(AccessTokenConfiguration tokenConfiguration)
        {
            TokenValidationParameters = new()
            {
                ValidateIssuerSigningKey = tokenConfiguration.Secret != null,
                ValidateIssuer = true,
                ValidIssuer = tokenConfiguration.Issuer,
                ValidateAudience = true,
                ValidAudience = tokenConfiguration.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            if (tokenConfiguration.Secret != null)
            {
                TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguration.Secret));
            }
        }

        public string GetTokenFromHeader(ActionContext context)
        {
            var accessToken = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Substring(7) ?? "";
            return accessToken;
        }

        public string GetTokenFromCookies(ActionContext context)
        {
            var accessToken = context.HttpContext.Request.Cookies["AccessToken"] ?? "";
            return accessToken;
        }

        public JwtSecurityToken? ValidateToken(string token)
        {
            if (token == "")
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, TokenValidationParameters, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                return jwtToken;
            }
            catch
            {
                return null;
            }
        }

        public bool HasTokenRole(JwtSecurityToken token, string role)
        {
            if (token == null)
            {
                return false;
            }
            var claims = token.Claims;

            var isRole = claims.Any(c => c.Type == ClaimTypes.Role && c.Value == role);
            if (!isRole)
            {
                return false;
            }
            return true;
        }

        public string GetRole(JwtSecurityToken jwtToken)
        {
            var roleClaims = jwtToken.Claims.Where(claim => claim.Type == ClaimTypes.Role).ToList();

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

        public string GetRoleFromCookieToken(ActionContext context)
        {
            var accessToken = context.HttpContext.Request.Cookies["AccessToken"];
            if (accessToken == null)
            {
                return "";
            }
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(accessToken);

            var roleClaims = jwtToken.Claims.Where(claim => claim.Type == ClaimTypes.Role).ToList();
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
