using System.ComponentModel.DataAnnotations;

namespace ContactsNotebook.Wpf.Services.Validation
{
    public class ModelValidator(IModelState modelState) : IModelValidator
    {
        private readonly IModelState _modelState = modelState;

        public IModelState Validate(object objectToValidate)
        {
            var validationContext = new ValidationContext(objectToValidate);
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(objectToValidate, validationContext, validationResults, true);

            return _modelState.LoadFromValidationResult(validationResults);
        }
    }
}
