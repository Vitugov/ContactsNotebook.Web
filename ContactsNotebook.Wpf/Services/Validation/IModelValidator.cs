namespace ContactsNotebook.Wpf.Services.Validation
{
    public interface IModelValidator
    {
        IModelState Validate(object objectToValidate);
    }
}
