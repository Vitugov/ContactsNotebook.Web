using System.Windows.Input;

namespace ContactsNotebook.Wpf.Commands
{
    interface ILoginCommand : ICommand
    {
        Task ExecuteAsync(object? parameter);

        Task<bool> CanExecuteAsync(object? parameter);
    }
}
