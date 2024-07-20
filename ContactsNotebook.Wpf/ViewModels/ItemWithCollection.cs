using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WPFUsefullThings;

namespace ContactsNotebook.Wpf.ViewModels
{
    public class ItemWithCollection<T> : INotifyPropertyChangedPlus, IItemWithCollection<T>
        where T : class, new()
    {
        private ObservableCollection<T> _itemCollection;
        public ObservableCollection<T> ItemCollection
        {
            get => _itemCollection;
            set => Set(ref _itemCollection, value);
        }

        private T? _selectedItem = default;
        public T? SelectedItem
        {
            get => _selectedItem;
            set => Set(ref _selectedItem, value);
        }

        public ItemWithCollection(ICollection<T> itemCollection)
        {
            ItemCollection = [.. itemCollection];
        }
    }
}
