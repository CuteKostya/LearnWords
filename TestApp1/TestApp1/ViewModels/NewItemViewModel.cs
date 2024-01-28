using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using TestApp1.Models;
using Xamarin.Forms;

namespace TestApp1.ViewModels
{
    [QueryProperty(nameof(DictionaryId), nameof(DictionaryId))]
    public class NewItemViewModel : BaseViewModel
    {
        private string _word;
        private string _translation;
        private int _dictionaryId;

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
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

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(_word)
                && !String.IsNullOrWhiteSpace(_translation);
        }

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

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Item newItem = new Item()
            {
                Word = Word,
                Translation = Translation,
                DictionaryId = DictionaryId
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
