using System.ComponentModel;
using TestApp1.ViewModels;
using Xamarin.Forms;

namespace TestApp1.Views
{
    public partial class EditDetailPage : ContentPage
    {
        public EditDetailPage()
        {
            InitializeComponent();
            BindingContext = new EditDetailViewModel();
        }
    }
}