using System;
using Tickets3.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tickets3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StaffWorkPage : ContentPage
    {
        public StaffWorkPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var staff = await App.Database.GetAllStaffAsync();

            staffPicker.ItemsSource = staff;

            listView.ItemsSource = await App.Database.GetAllWorkForStaffMember(1);
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var obj = (Work)e.SelectedItem;
                var ide = Convert.ToInt32(obj.ID);

                await Navigation.PushAsync(new TimeClockPage(ide)
                );
            }
        }

        async void staffPicker_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var staff = await App.Database.GetAllStaffAsync();

            var staffID = staff[staffPicker.SelectedIndex].ID;

            listView.ItemsSource = await App.Database.GetAllWorkForStaffMember(staffID);
        }

        private void ToolBarSignOutButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}