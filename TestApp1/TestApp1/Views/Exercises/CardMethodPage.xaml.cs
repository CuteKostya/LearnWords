using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TestApp1.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp1.Views
{
    public partial class CardMethodPage : ContentPage
    {
        CardMethodViewModel _viewModel;

        public CardMethodPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new CardMethodViewModel();

            //TheCarousel.ItemsSource = ;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

    }
}