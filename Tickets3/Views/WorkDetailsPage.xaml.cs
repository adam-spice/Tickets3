using System;
using Tickets3.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tickets3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkDetailsPage : ContentPage
    {
        public WorkDetailsPage()
        {
            InitializeComponent();
        }

        public Client SelectedClient { get; set; }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            clientPicker.ItemsSource = await App.Database.GetClientsAsync();
            staffPicker.ItemsSource = await App.Database.GetAllStaffAsync();

            var work = BindingContext as Work;

            clientPicker.SelectedIndex = work.ClientID;
            staffPicker.SelectedIndex = work.StaffID;

        }



        async void OnSaveClicked(object sender, EventArgs e)
        {
            var clients = await App.Database.GetClientsAsync();
            var staff = await App.Database.GetAllStaffAsync();




            var work = new Work()
            {
                ClientID = clients[clientPicker.SelectedIndex].ID,
                ClientName = clients[clientPicker.SelectedIndex].Name,
                StaffID = staff[staffPicker.SelectedIndex].ID,
                StaffName = staff[staffPicker.SelectedIndex].Name
            };


            await App.Database.SaveWorkAsync(work);
            await Navigation.PopAsync();
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var work = (Work)BindingContext;
            await App.Database.DeleteWorkAsync(work);
            await Navigation.PopAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void staffPicker_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}