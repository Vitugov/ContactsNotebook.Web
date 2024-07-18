using ContactsNotebook.Lib.Models.Identity;

namespace ContactsNotebook.Lib.Services.ApiClients.Authentication
{
    public interface IAuthenticationApiClient
    {
        public Task<bool> RegisterUserAsync(RegisterViewModel registerViewModel);

        public Task<TokenResponse?> LoginUserAsync(LoginViewModel loginViewModel);
        public Task<bool> LogoutUserAsync();
        public Task<string> GetUsersAsync();
        public Task<bool> DeleteUserAsync(Guid id);
    }
}
