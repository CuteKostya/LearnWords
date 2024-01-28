using System;
using System.Diagnostics;
using System.Windows.Input;
using TestApp1.Models;
using TestApp1.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TestApp1.ViewModels
{
    [QueryProperty(nameof(DictionaryId), nameof(DictionaryId))]
    public class ExercisesViewModel : BaseViewModel
    {
        private int _dictionaryId;
        public Command<Item> CardMethodCommand { get; }
        public Command<Item> ChoiceMethodCommand { get; }
        public ICommand OpenWebCommand { get; }

        public ExercisesViewModel()
        {
            Title = "Упражнения";
            CardMethodCommand = new Command<Item>(OnCardMethod);
            ChoiceMethodCommand = new Command<Item>(OnChoiceMethod);
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
        private async void OnCardMethod(Item item)
        {
            await Shell.Current.GoToAsync(nameof(CardMethodPage));
        }
        private async void OnChoiceMethod(Item item)
        {
            await Shell.Current.GoToAsync(nameof(ChoiceMethodPage));
        }
    }
}