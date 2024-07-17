using Microsoft.AspNetCore.Identity;

namespace AuthenticationServer.Api.Models.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string LastToken { get; set; } = string.Empty;
    }
}
