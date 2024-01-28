using System.ComponentModel;
using TestApp1.ViewModels;
using Xamarin.Forms;

namespace TestApp1.Views
{
    public partial class DictionaryDetailPage : ContentPage
    {
        public DictionaryDetailPage()
        {
            InitializeComponent();
            BindingContext = new DictionaryDetailViewModel();
        }
    }
}