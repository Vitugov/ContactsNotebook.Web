using AuthenticationServer.Api.Models.Configuration;
using AuthenticationServer.Api.Models.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationServer.Api.Services.TokenGenerator
{
    public class AccessTokenGenerator(AccessTokenConfiguration configuration) : IAccessTokenGenerator
    {
        private readonly AccessTokenConfiguration _configuration = configuration;
        public string GenerateToken(ApplicationUser user, IList<string> roles)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            List<Claim> claims =
            [
                new("id", user.Id.ToString()),
                new(ClaimTypes.Name, user.Email!)
            ];
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var token = new JwtSecurityToken(
                _configuration.Issuer,
                _configuration.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(_configuration.ExpirationMinutes),
                credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
