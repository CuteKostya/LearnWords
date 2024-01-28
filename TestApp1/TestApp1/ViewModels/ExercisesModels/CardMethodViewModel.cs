using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestApp1.Models;
using TestApp1.Views;
using Xamarin.Forms;

namespace TestApp1.ViewModels
{
    [QueryProperty(nameof(DictionaryId), nameof(DictionaryId))]
    public class CardMethodViewModel : BaseViewModel
    {
        private int _dictionaryId;
        private Item _selectedItem;
        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command WordLearnedCommad { get; }
        public Command WordStudyCommad { get; }

        public CardMethodViewModel()
        {
            Title = "Карточки";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            WordLearnedCommad = new Command<Item>(OnWordLearnedSelected);
            WordStudyCommad = new Command<Item>(OnWordWordStudySelected);
        }

        private async void OnWordLearnedSelected(Item item)
        {
            Item newItem = await DataStore.GetItemAsync(item.Id);
            newItem.Studied = true;
            await DataStore.UpdateItemAsync(newItem);
        }
        private async void OnWordWordStudySelected(Item item)
        {
            Item newItem = await DataStore.GetItemAsync(item.Id);
            newItem.BeingStudied = true;
            await DataStore.UpdateItemAsync(newItem);
        }

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
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                IEnumerable<Item> items = await DataStore.GetItemsNotStudiedAndBeingStudiedTenAsync(DictionaryId);
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
            ExecuteLoadItemsCommand();
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
        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}