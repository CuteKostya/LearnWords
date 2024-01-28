using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TestApp1.Models;
using Xamarin.Forms;

namespace TestApp1.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private int _itemId;
        private string _word;
        private string _translation;
        public int Id { get; set; }

        public string Word
        {
            get => _word;
            set => SetProperty(ref _word, value);
        }

        public string Translation
        {
            get => _translation;
            set => SetProperty(ref _translation, value);
        }

        public int ItemId
        {
            get
            {
                return _itemId;
            }
            set
            {
                _itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Word = item.Word;
                Translation = item.Translation;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
