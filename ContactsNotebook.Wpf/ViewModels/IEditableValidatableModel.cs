using ContactsNotebook.Wpf.Services.Validation;

namespace ContactsNotebook.Wpf.ViewModels
{
    public interface IEditableValidatableModel<T> where T : class, new()
    {
        T? Current { get; set; }
        IModelState ModelState { get; set; }
        bool ShowValidationErrors { get; set; }

        bool Save();
    }
}