using ContactsNotebook.Wpf.Commands;
using ContactsNotebook.Wpf.Services.EntityManipulations;
using ContactsNotebook.Wpf.Services.Validation;
using ContactsNotebook.Wpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace ContactsNotebook.Wpf.Services.ViewModelsFactories
{
    class ViewModelsFactory(IServiceProvider serviceProvider)
    {
        private readonly IServiceProvider serviceProvider = serviceProvider;

        public IEditableValidatableModel<T> CreateEditableValidatableModel<T>
            (T? original = null, ObservableCollection<T>? collection = null)
            where T : class, new()
        {
            var validator = serviceProvider.GetRequiredService<IModelValidator>();
            var cloner = serviceProvider.GetRequiredService<ICloner>();
            var result = new EditableValidatableModel<T>(validator, cloner, original, collection);
            return result;
        }

        public ILoginVM<T> CreateLoginVM<T>(IEditableValidatableModel<T> editableValidatableModel)
            where T : class, new()
        {
            var loginCommand = serviceProvider.GetRequiredService<ILoginCommand>();
            var loginAnonymousCommand = serviceProvider.GetRequiredService<ILoginAnonymousCommand>();
            var result = new LoginVM<T>(editableValidatableModel, loginCommand, loginAnonymousCommand);
            return result;
        }

        public ILoginVM<T> CreateLoginVM<T>()
            where T : class, new()
        {
            var editableValidatableModel = CreateEditableValidatableModel<T>(null, null);
            var loginVM = CreateLoginVM(editableValidatableModel);
            return loginVM;
        }
    }
}
