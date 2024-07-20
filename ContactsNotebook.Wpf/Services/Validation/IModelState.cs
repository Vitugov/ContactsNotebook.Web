using System.ComponentModel.DataAnnotations;

namespace ContactsNotebook.Wpf.Services.Validation
{
    public interface IModelState : IDictionary<string, List<string>>
    {
        abstract IModelState LoadFromValidationResult(ICollection<ValidationResult> validationResults);
        virtual bool Success => Count == 0;
    }
}
