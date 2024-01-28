using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TestApp1.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp1.Views
{
    public partial class ChoiceMethodPage : ContentPage
    {
        ChoiceMethodViewModel _viewModel;

        public ChoiceMethodPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ChoiceMethodViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

    }
}