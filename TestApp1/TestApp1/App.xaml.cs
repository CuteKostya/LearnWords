using System;
using System.IO;
using Notes.Data;
using TestApp1;
using Xamarin.Forms;

namespace TestApp1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Register<NoteDatabase>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}