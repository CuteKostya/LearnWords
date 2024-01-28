using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TestApp1.Models;
using TestApp1.Views;
using System.IO;
using Newtonsoft.Json;

namespace TestApp1.ViewModels
{
    public class DictionaryViewModel : BaseViewModel
    {
        private DictionaryBoFileRead[] _dictionaries;
        public ObservableCollection<DictionaryBo> Dictionaries { get; }
        private string _pathFileDictionary;
        public Command AddDictionaryCommand { get; }
        private DictionaryBo _selectedDictionary;
        public Command LoadDictionariesCommand { get; }
        public Command<DictionaryBo> ItemTapped { get; }

        public DictionaryViewModel()
        {
            Title = "Словари";
            AddDictionaryCommand = new Command(OnDictionaryItem);

            ItemTapped = new Command<DictionaryBo>(OnItemSelected);

            Dictionaries = new ObservableCollection<DictionaryBo>();
            LoadDictionariesCommand = new Command(async () => await ExecuteLoadDictionariesCommand());

        }
        async Task ExecuteLoadDictionariesCommand()
        {
            IsBusy = true;

            try
            {
                Dictionaries.Clear();
                IEnumerable<DictionaryBo> dictionaries = await DataStore.GetDictionariesAsync(true);
                foreach (DictionaryBo dictionary in dictionaries)
                {
                    Dictionaries.Add(dictionary);
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

        public DictionaryBo SelectedItem
        {
            get => _selectedDictionary;
            set
            {
                SetProperty(ref _selectedDictionary, value);
                OnItemSelected(value);
            }
        }
        async void OnItemSelected(DictionaryBo dictionary)
        {
            if (dictionary == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(DictionaryDetailPage)}?{nameof(DictionaryDetailViewModel.DictionaryId)}={dictionary.Id}");
        }

        private async void OnDictionaryItem(object obj)
        {
            try
            {
                //var file = await FilePicker.PickAsync();

                //if (file == null)
                //    return;
                //_pathFileDictionary = file.FullPath;

                _pathFileDictionary =
                    "/storage/emulated/0/Android/data/com.companyname.testapp1/cache/2203693cc04e0be7f4f024d5f9499e13/9211d720ee7749b5a41c0f7e35b1f232/dictionaries.json";

                using (StreamReader reader = new StreamReader(_pathFileDictionary))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    _dictionaries = (DictionaryBoFileRead[])serializer.Deserialize(reader, typeof(DictionaryBoFileRead[]));
                }


                DictionaryBo newDictionary = new DictionaryBo();

                newDictionary.Title = _dictionaries[0].Title;
                newDictionary.Description = _dictionaries[0].Description;
                newDictionary.MyLanguage = _dictionaries[0].MyLanguage;
                newDictionary.LearningLanguage = _dictionaries[0].LearningLanguage;

                await DataStore.AddDictionaryAsync(newDictionary);
                for (int i = 0; i < _dictionaries[0].Words.Length; i++)
                {
                    Item newItems = new Item();
                    newItems.DictionaryId = newDictionary.Id;
                    newItems.Word = _dictionaries[0].Words[i].Word;
                    newItems.Transcription = _dictionaries[0].Words[i].Transcription;
                    newItems.Translation = _dictionaries[0].Words[i].Translation;

                    await DataStore.AddItemAsync(newItems);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}