using LuckDraw.Windows;
using System.Windows;
using System.Windows.Controls;

namespace LuckDraw
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary
    public partial class App : Application
    {
        internal static Settings settings = new(55);
        internal static new MainWindow? MainWindow = new();
        internal static FloatingWindow FloatingWindow = new();
        internal static Frame ContentFrame = new();


        private void Application_Startup(object sender, StartupEventArgs e)
        {
            SplashWindow window = new();
            window.Show();
            Settings.Load();
            FloatingWindow.Show();
        }
    }
}
