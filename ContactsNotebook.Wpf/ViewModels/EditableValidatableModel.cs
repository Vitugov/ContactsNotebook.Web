using ContactsNotebook.Wpf.Services.EntityManipulations;
using ContactsNotebook.Wpf.Services.Validation;
using System.Collections.ObjectModel;
using WPFUsefullThings;

namespace ContactsNotebook.Wpf.ViewModels
{
    public class EditableValidatableModel<T> : INotifyPropertyChangedPlus, IEditableValidatableModel<T>
        where T : class, new()
    {
        private readonly IModelValidator validator;
        private readonly ICloner cloner;
        private readonly T? original;
        private T? current;
        public T? Current
        {
            get => current;
            set => Set(ref current, value);
        }
        private IModelState modelState;
        public IModelState ModelState
        {
            get => modelState;
            set => Set(ref modelState, value);
        }
        private bool showValidationErrors;
        public bool ShowValidationErrors
        {
            get => showValidationErrors;
            set => Set(ref showValidationErrors, value);
        }
        private readonly ObservableCollection<T>? collection;
        private readonly bool hasCollection;
        private readonly bool isNew;

        public EditableValidatableModel(IModelValidator validator, ICloner cloner, T? original = null,
            ObservableCollection<T>? collection = null)
        {
            hasCollection = collection != null;
            isNew = original == null;
            this.validator = validator;
            this.cloner = cloner;
            this.original = original ?? new T();
            this.collection = collection;
            Current = cloner.Clone<T>(original!);
            modelState = validator.Validate(Current!);
            ShowValidationErrors = false;
        }

        public bool Save()
        {
            ModelState = validator.Validate(Current!);
            if (!ModelState.Success)
            {
                ShowValidationErrors = true;
                return false;
            }
            else
            {
                cloner.Update(original, Current);
                HandleCollection();
                return true;
            }
        }

        private void HandleCollection()
        {
            if (hasCollection)
            {
                if (!isNew)
                {
                    collection!.Remove(original!);
                }
                collection!.Add(original!);
            }
        }
    }
}
