using Xamarin.Forms;

namespace LuckDrawXamarin
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            if (!Current.Properties.ContainsKey("number"))
            {
                Current.Properties.Add("number", 55);
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
