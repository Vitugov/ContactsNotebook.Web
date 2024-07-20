using ContactsNotebook.Wpf.Commands;
using System.Windows.Input;

namespace ContactsNotebook.Wpf.ViewModels
{
    class LoginVM<T>(IEditableValidatableModel<T> editableValidatableModel, ILoginCommand loginCommand, ILoginAnonymousCommand loginAnonymousCommand) : ILoginVM<T>
        where T : class, new()
    {
        public IEditableValidatableModel<T> Data { get; set; } = editableValidatableModel;
        public ICommand LoginCommand { get; set; } = loginCommand;
        public ICommand EnterAnonymousCommand { get; set; } = loginAnonymousCommand;
    }
}
