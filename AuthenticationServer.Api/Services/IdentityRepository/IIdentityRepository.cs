using AuthenticationServer.Api.Models.Identity;
using ContactsNotebook.Models.Identity;

namespace AuthenticationServer.Api.Services.IdentityRepository
{
    public interface IIdentityRepository
    {
        public Task<ApplicationUser?> CreateUserAsync(string userName, string userEmail, string userPassword, string[] roles);
        public Task<bool> DeleteUserAsync(Guid id);
        public Task<ApplicationUser?> GetUserByEmailAsync(string email);
        public Task<bool> DoesUserExistAsync(string email);
        public Task<bool> DoesUserExistAsync(Guid id);
        public Task<IEnumerable<UserView>> GetAllUsersAsync();
        public Task<ApplicationUser?> LoginAsync(string username, string password, bool isPersistent);
        public Task<IList<string>> GetRolesAsync(ApplicationUser user);
        public Task<bool> RefreshToken(ApplicationUser user, string token);
        public Task LogOutAsync();
        public ApplicationUser? FindUserByToken(string? token);
    }
}
