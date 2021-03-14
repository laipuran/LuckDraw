using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using Application = System.Windows.Application;

namespace LuckDrawWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static int numberOfPeople;
        internal static bool doShowToasts;
        internal static bool closeApp = false;
        internal static Window FloatingWindow = new Floating();
        internal static NotifyIcon trayIcon;
        internal static Window Window = new MainWindow();
        protected override void OnStartup(StartupEventArgs e)
        {
            ShowSplashScreen();
            AddTrayIcon();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            closeApp = true;
            RemoveTrayIcon();
            base.OnExit(e);
        }
        private void ShowSplashScreen()
        {
            SplashScreen s = new SplashScreen("Resources/SplashScreen.png");
            s.Show(true);
        }
        private void AddTrayIcon()
        {
            Icon icon = LuckDrawWPF.Properties.Resources.favicon;
            if (trayIcon != null)
            {
                return;
            }
            trayIcon = new NotifyIcon
            {
                Icon = icon,
                Text = "Luck Draw by Puran Lai"
            };
            trayIcon.Visible = true;

            //实例化右键菜单
            ContextMenuStrip menuStrip = new ContextMenuStrip();

            menuStrip.Items.Add("显示主窗口", null, (sender, eventArgs) => {
                MainWindow.Visibility = Visibility.Visible;
                MainWindow.ShowInTaskbar = true;
                MainWindow.Activate();
            });
            menuStrip.Items.Add("退出", null, (sender, eventArgs) => {
                closeApp = true;
                MainWindow.Close();
            });
            
            trayIcon.ContextMenuStrip = menuStrip;
        }
        public void RemoveTrayIcon()
        {
            if (trayIcon != null)
            {
                trayIcon.Visible = false;
                trayIcon.Dispose();
                trayIcon = null;
            }
        }
    }
}
