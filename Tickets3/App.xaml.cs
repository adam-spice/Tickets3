using Tickets3.Data;
using Tickets3.Views;
using Xamarin.Forms;

namespace Tickets3
{
    public partial class App : Application
    {
        static TicketDatabase database;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }


        public static TicketDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new TicketDatabase();
                }
                return database;
            }
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
