using System.Windows.Input;

namespace ContactsNotebook.Wpf.ViewModels
{
    internal interface ILoginVM<T>
        where T : class, new()
    {
        IEditableValidatableModel<T> Data { get; set; }
        ICommand EnterAnonymousCommand { get; set; }
        ICommand LoginCommand { get; set; }
    }
}