
using System;
using Tickets3.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tickets3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkPage : ContentPage
    {
        public WorkPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            listView.ItemsSource = await App.Database.GetAllWorkAsync();
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WorkDetailsPage
            {
                BindingContext = new Work()
            });
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new WorkDetailsPage
                {
                    BindingContext = e.SelectedItem as Work
                });
            }
        }

        private void ToolBarSignOutButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}