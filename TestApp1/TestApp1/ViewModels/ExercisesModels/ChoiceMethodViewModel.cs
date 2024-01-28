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
    public class ChoiceMethodViewModel : BaseViewModel
    {
        private int _dictionaryId;
        private Choice _selectedItem;
        public ObservableCollection<Choice> Choices { get; }
        public Command LoadItemsCommand { get; }
        public Command TranslationCheckCommand { get; }

        public ChoiceMethodViewModel()
        {
            // RemoveCommand = new Command(Remove);
            Title = "Выбор перевода";

            Choices = new ObservableCollection<Choice>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            TranslationCheckCommand = new Command(OnTranslationCheckMethod);
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

        private async void OnTranslationCheckMethod(object item)
        {
            ChoiceButton choiceButton = (ChoiceButton)item;
            choiceButton.Choice.PressedButton = choiceButton.Button;
            Item newItem = await DataStore.GetItemAsync(choiceButton.Choice.Id);
            if (choiceButton.Button == choiceButton.Choice.Translation)
                newItem.Score += 3;
            else
                newItem.Score -= 2;

            if (newItem.Score < 0)
                newItem.Score = 0;

            if (newItem.Score == 20)
                newItem.Studied = true;

            await DataStore.UpdateItemAsync(newItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Choices.Clear();

                IEnumerable<Item> itemsList = await DataStore.GetItemsNotStudiedTenAsync(DictionaryId);

                Random rnd = new Random();
                IEnumerable<Item> items = itemsList.OrderBy(x => rnd.Next()).ToList();

                int k = 0;
                string[] massTranslatStrings = new string[items.Count()];
                foreach (Item item in items)
                {
                    massTranslatStrings[k] = item.Translation;
                    k++;
                    if(k>=10) break;
                }
                foreach (Item item in items)
                {
                    Choice choices = new Choice();
                    choices.Id = item.Id;
                    choices.Word = item.Word;
                    choices.Translation = item.Translation;
                    choices.MassTranslation = new string[5];
                    bool meetingSymbol = false;
                    for (int i = 0; i < 5; i++)
                    {
                        if (massTranslatStrings[i] != choices.Translation)
                            meetingSymbol = true;
                        if (!meetingSymbol)
                            choices.MassTranslation[i] = massTranslatStrings[i];
                        else
                            choices.MassTranslation[i] = massTranslatStrings[i + 1];
                    }
                    for (int i = choices.MassTranslation.Length - 1; i >= 1; i--)
                    {
                        int j = rnd.Next(i + 1);
                        // обменять значения data[j] и data[i]
                        var temp = choices.MassTranslation[j];
                        choices.MassTranslation[j] = choices.MassTranslation[i];
                        choices.MassTranslation[i] = temp;
                    }
                    Choices.Add(choices);
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

        public Choice SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }
        async void OnItemSelected(Choice item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}