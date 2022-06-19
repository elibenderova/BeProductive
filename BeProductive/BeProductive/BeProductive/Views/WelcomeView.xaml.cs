using BeProductive.Models;
using BeProductive.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeProductive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomeView : ContentPage
    {

        public WelcomeView()
        {
            InitializeComponent();
        }

        private async void GoButton_Clicked(object sender, EventArgs e)
        {
            activityIndicator.IsRunning = true;

            var mainView = new MainPage();
            await Navigation.PushAsync(mainView);
        }
    }
}