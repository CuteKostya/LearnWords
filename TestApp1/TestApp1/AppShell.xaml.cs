using System;
using System.Collections.Generic;
using TestApp1.ViewModels;
using TestApp1.Views;
using Xamarin.Forms;

namespace TestApp1
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(DictionaryDetailPage), typeof(DictionaryDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(ItemsPage), typeof(ItemsPage));
            Routing.RegisterRoute(nameof(ExercisesPage), typeof(ExercisesPage));
            Routing.RegisterRoute(nameof(EditDetailPage), typeof(EditDetailPage));
            Routing.RegisterRoute(nameof(CardMethodPage), typeof(CardMethodPage)); 
            Routing.RegisterRoute(nameof(ChoiceMethodPage), typeof(ChoiceMethodPage));
        }

    }
}
