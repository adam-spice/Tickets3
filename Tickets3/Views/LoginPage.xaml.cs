using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tickets3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }



        async void supervisorButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SupervisorDashboardPage());
        }

        async void staffButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StaffWorkPage());
        }
    }
}