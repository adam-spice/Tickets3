
using System;
using System.Timers;
using Tickets3.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tickets3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimeClockPage : ContentPage
    {
        private Work selectedWork
        {
            get; set;
        }

        public int SelectedWorkID { get; set; }

        private Timer _timer;
        public TimeSpan RunningTotal { get; set; }
        public DateTime CurrentStartTime { get; private set; }
        public DateTime CurrentEndTime { get; private set; }
        public bool Working { get; private set; }


        public TimeClockPage(int id)
        {
            InitializeComponent();

            SelectedWorkID = id;
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Enabled = false;
            _timer.Elapsed += _timer_Elapsed;

            RunningTotal = new TimeSpan();
            runningTotalLabel.Text = RunningTotal.ToString(@"hh\:mm\:ss");
            stopStartButton.Text = "Start Work";


        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            selectedWork = await App.Database.GetWorkAsync(SelectedWorkID);
        }


        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            RunningTotal += TimeSpan.FromSeconds(1);
            Device.BeginInvokeOnMainThread(() => runningTotalLabel.Text = RunningTotal.ToString(@"hh\:mm\:ss"));
        }

        private async void stopStartButton_Clicked(object sender, EventArgs e)
        {
            if (Working)
            {
                _timer.Enabled = false;
                RunningTotal = TimeSpan.Zero;
                stopStartButton.Text = "Start Work";
                selectedWork.Start = CurrentStartTime;
                selectedWork.End = DateTime.Now;

                await App.Database.SaveWorkAsync(selectedWork);


            }
            else
            {
                CurrentStartTime = DateTime.Now;
                _timer.Enabled = true;
                stopStartButton.Text = "Stop Work";
            }

            Working = !Working;
        }

        private void cancelButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StaffWorkPage());
        }

        private void ToolBarSignOutButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}