using ContactsNotebook.Lib.Models.Identity;

namespace ContactsNotebook.Wpf.Commands
{
    public class LoginAnonymousCommand(IAppUser user) : AsyncCommand, ILoginAnonymousCommand
    {
        private readonly IAppUser _user = user;

        public override async Task ExecuteAsync(object? parameter)
        {
        }

        public override Task<bool> CanExecuteAsync(object? parameter)
        {

            return base.CanExecuteAsync(parameter);
        }
    }
}
