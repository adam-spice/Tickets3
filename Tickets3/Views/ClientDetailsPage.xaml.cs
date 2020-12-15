using System;
using Tickets3.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tickets3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientDetailsPage : ContentPage
    {
        public ClientDetailsPage()
        {
            InitializeComponent();
        }
        async void OnSaveClicked(object sender, EventArgs e)
        {
            var client = (Client)BindingContext;
            await App.Database.SaveClientAsync(client);
            await Navigation.PopAsync();
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var client = (Client)BindingContext;
            await App.Database.DeleteClientAsync(client);
            await Navigation.PopAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}