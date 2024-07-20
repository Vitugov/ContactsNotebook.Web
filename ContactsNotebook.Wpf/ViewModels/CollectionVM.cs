using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using WPFUsefullThings;

namespace ContactsNotebook.Wpf.ViewModels
{
    public class CollectionVM<T> : INotifyPropertyChangedPlus
        where T : class, new()
    {
        public string Header { get; set; }

        private ListCollectionView _itemCollectionView;

        public ListCollectionView ItemCollectionView
        {
            get => _itemCollectionView;
            set => Set(ref _itemCollectionView, value);
        }

        public IItemWithCollection<T> ItemWithCollection {  get; set; }

        private string _filterText = "";
        public string FilterText
        {
            get => _filterText;
            set
            {
                Set(ref _filterText, value);
                //ItemCollectionView.Filter = obj => obj.IsContainingString(FilterText);
            }
        }

        public ICommand AddNewItemCommand { get; }
        public ICommand ViewItemCommand { get; }
        public ICommand ChangeItemCommand { get; }
        public ICommand DeleteItemCommand { get; }

        public CollectionVM(IItemWithCollection<T> itemWithCollection, string header)
        {
            Header = header;
            ItemWithCollection = itemWithCollection;
            ItemCollectionView = new ListCollectionView(ItemWithCollection.ItemCollection);
            ItemCollectionView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));

            AddNewItemCommand = new RelayCommand(obj => ExecuteChangeItem());
            ChangeItemCommand = new RelayCommand(obj => ExecuteChangeItem(SelectedItem), obj => SelectedItem != null);
            DeleteItemCommand = new RelayCommand(obj => ExecuteDeleteItem(SelectedItem), obj => SelectedItem != null);
        }

        private void ExecuteChangeItem(T? item = null)
        {
            var itemVM = new ItemViewModel<T>(item, ItemCollection);
            var itemView = new ItemWindow<T>(itemVM);
            itemView.ShowDialog();
        }

        private void ExecuteDeleteItem(T? item)
        {
            if (item == null) { return; }
            var list = item.GetLinksOnItem();
            if (list.Any())
            {
                MessageBox.Show($"Невозможно удалить объект. На данный объект есть {list.Count} ссылок в базе данных.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ItemCollection.Remove(item);
            item.DeleteItem();
        }
    }
}
