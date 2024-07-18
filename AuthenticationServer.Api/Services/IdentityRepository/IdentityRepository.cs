using AuthenticationServer.Api.Models.Identity;
using ContactsNotebook.Lib.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationServer.Api.Services.IdentityRepository
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IdentityRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ApplicationUser?> CreateUserAsync(string userName, string userEmail, string userPassword, string[] roles)
        {
            var user = new ApplicationUser() { UserName = userName, Email = userEmail };
            var result = await _userManager.CreateAsync(user, userPassword);
            if (result.Succeeded)
            {
                foreach (var role in roles)
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
                return user;
            }
            return null;
        }
        public async Task<bool> DoesUserExistAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return false;
            }
            await _userManager.DeleteAsync(user);
            return true;
        }

        public async Task<IEnumerable<UserView>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();
            var result = new List<UserView>();
            foreach (var user in users)
            {
                var isAdmin = await _userManager.IsInRoleAsync(user, "Administrator");
                var userView = new UserView()
                {
                    Id = user.Id.ToString(),
                    Email = user.Email,
                    IsAdmin = isAdmin
                };
                result.Add(userView);
            }
            return result;
        }

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DoesUserExistAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return user != null;
        }

        public async Task<ApplicationUser?> LoginAsync(string username, string password, bool isPersistent)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return null;
            }
            var user = await _userManager.FindByNameAsync(username);
            return user;
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user) => await _userManager.GetRolesAsync(user);

        public async Task<bool> RefreshToken(ApplicationUser user, string token)
        {
            user.LastToken = token;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
            return;
        }

        public ApplicationUser? FindUserByToken(string? token)
        {
            if (token == null)
            {
                return null;
            }
            var users = _userManager.Users.ToList();
            var user = _userManager.Users.Where(user => user.LastToken == token).FirstOrDefault();
            return user;
        }

        public async Task<ApplicationUser?> FindUserByGuid(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return user;
        }
    }
}
