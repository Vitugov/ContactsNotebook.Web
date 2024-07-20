using ContactsNotebook.Lib.Models.Identity;
using ContactsNotebook.Lib.Services.ApiClients.Authentication;
using ContactsNotebook.Wpf.ViewModels;

namespace ContactsNotebook.Wpf.Commands
{
    public class LoginCommand(IAuthenticationApiClient authenticationApiClient, IAppUser user) : AsyncCommand, ILoginCommand
    {
        private readonly IAuthenticationApiClient _authenticationApiClient = authenticationApiClient;
        private readonly IAppUser _user = user;

        public override async Task ExecuteAsync(object? parameter)
        {
            var editableValidatableModel = (IEditableValidatableModel<LoginViewModel>)parameter!;
            var success = editableValidatableModel.Save();
            if (!success)
            {
                return;
            }
            var tokenResponse = await _authenticationApiClient.LoginUserAsync(editableValidatableModel.Current!);
            _user.AccessToken = tokenResponse?.AccessToken!;
        }

        public override Task<bool> CanExecuteAsync(object? parameter)
        {

            return base.CanExecuteAsync(parameter);
        }
    }
}
