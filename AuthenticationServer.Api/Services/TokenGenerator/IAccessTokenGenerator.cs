using AuthenticationServer.Api.Models.Identity;

namespace AuthenticationServer.Api.Services.TokenGenerator
{
    public interface IAccessTokenGenerator
    {
        public string GenerateToken(ApplicationUser user, IList<string> role);
    }
}
