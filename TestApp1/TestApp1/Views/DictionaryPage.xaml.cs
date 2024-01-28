using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp1.Models;
using TestApp1.ViewModels;
using TestApp1.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp1.Views
{
    public partial class DictionaryPage : ContentPage
    {
        DictionaryViewModel _viewModel;
        public DictionaryPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new DictionaryViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}