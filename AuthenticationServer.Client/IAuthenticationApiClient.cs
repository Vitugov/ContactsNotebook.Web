using ContactsNotebook.Models.Identity;

namespace AuthenticationServer.ApiClient
{
    public interface IAuthenticationApiClient
    {
        public Task<bool> RegisterUserAsync(RegisterViewModel registerViewModel);

        public Task<TokenResponse?> LoginUserAsync(LoginViewModel loginViewModel);
        public Task<bool> LogoutUserAsync();
    }
}
