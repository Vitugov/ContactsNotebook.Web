using System.Collections.ObjectModel;

namespace ContactsNotebook.Wpf.ViewModels
{
    public interface IItemWithCollection<T>
        where T : class, new()
    {
        ObservableCollection<T> ItemCollection { get; set; }
        T? SelectedItem { get; set; }
    }
}