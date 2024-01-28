using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TestApp1.Models;
using TestApp1.Views;
using Xamarin.Forms;

namespace TestApp1.ViewModels
{
    [QueryProperty(nameof(DictionaryId), nameof(DictionaryId))]
    public class ItemsViewModel : BaseViewModel
    {
        private int _dictionaryId;
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> EditItemCommand { get; }
        public Command<Item> ItemTapped { get; }
        public Command RemoveCommand { get; }
        public int DictionaryId
        {
            get
            {
                return _dictionaryId;
            }
            set
            {
                _dictionaryId = value;
            }
        }
        public ItemsViewModel()
        {
            RemoveCommand = new Command(Remove);
            Title = "Слова";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            EditItemCommand = new Command<Item>(OnEditItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                IEnumerable<Item> items = await DataStore.GetItemsNotStudiedTenAsync(DictionaryId);
                foreach (Item item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(NewItemPage)}?{nameof(NewItemViewModel.DictionaryId)}={DictionaryId}");
        }
        private async void OnEditItem(Item item)
        {

            if (item == null)
                return;
            await Shell.Current.GoToAsync($"{nameof(EditDetailPage)}?{nameof(EditDetailViewModel.ItemId)}={item.Id}");
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

        private void Remove(object obj)
        {
            Item item = obj as Item;
            if (item == null) return;

            DataStore.DeleteItemAsync(item.Id);

            Items.Remove(item);
        }
    }
}