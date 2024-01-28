using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TestApp1.Models;
using TestApp1.Views;
using Xamarin.Forms;

namespace TestApp1.ViewModels
{
    [QueryProperty(nameof(DictionaryId), nameof(DictionaryId))]
    public class DictionaryDetailViewModel : BaseViewModel
    {
        private int _dictionaryId;
        private string _title;
        private string _description;
        public Command<DictionaryBo> ExercisesTappedCommand { get; }
        public Command<DictionaryBo> ItemsTappedCommand { get; }
        public int Id { get; set; }
        public DictionaryDetailViewModel()
        {
            Title = "Мой словарьь";

            ExercisesTappedCommand = new Command<DictionaryBo>(OnExercisesTappedOn);
            ItemsTappedCommand = new Command<DictionaryBo>(OnItemsTappedOn);

        }
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
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
                LoadDictionaryId(value);
            }
        }
        async void OnExercisesTappedOn(DictionaryBo dictionary)
        {
            //if (dictionary == null)
            //    return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ExercisesPage)}?{nameof(ExercisesViewModel.DictionaryId)}={DictionaryId}");
        }
        async void OnItemsTappedOn(DictionaryBo dictionary)
        {
            //if (dictionary == null)
            //    return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemsPage)}?{nameof(ItemsViewModel.DictionaryId)}={DictionaryId}");
        }

        public async void LoadDictionaryId(int dictionaryId)
        {
            try
            {
                var dictionary = await DataStore.GetDictionaryAsync(dictionaryId);
                Id = dictionary.Id;
                Title = dictionary.Title;
                Description = dictionary.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
