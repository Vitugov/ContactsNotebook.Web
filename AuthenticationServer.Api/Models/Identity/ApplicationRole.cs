using Microsoft.AspNetCore.Identity;

namespace AuthenticationServer.Api.Models.Identity
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole(string role) : base(role) { }

        public ApplicationRole() : base() { }
    }
}
