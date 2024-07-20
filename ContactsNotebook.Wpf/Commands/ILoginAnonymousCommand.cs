using System.Windows.Input;

namespace ContactsNotebook.Wpf.Commands
{
    interface ILoginAnonymousCommand : ICommand
    {
        Task ExecuteAsync(object? parameter);

        Task<bool> CanExecuteAsync(object? parameter);
    }
}
