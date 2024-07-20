namespace ContactsNotebook.Wpf.Services.EntityManipulations
{
    public interface ICloner
    {
        T Clone<T>(T source) where T : class, new();
        void Update<T>(T target, T source);
    }
}