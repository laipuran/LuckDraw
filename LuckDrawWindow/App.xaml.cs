using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using Application = System.Windows.Application;

namespace LuckDrawWindow
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        internal static int numberOfPeople;
        internal static bool doShowToasts;
        internal static bool closeApp = false;
        internal static Window FloatingWindow = new Floating();
        private NotifyIcon trayIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            showSplashScreen();
            addTrayIcon();
            base.OnStartup(e);
        }
        private void ExitApp(object sender, EventArgs e)
        {
            closeApp = true;

            MainWindow.Close();
        }
        private void showSplashScreen()
        {
            SplashScreen s = new SplashScreen("SplashScreen.png");
            s.Show(true);
            s.Close(new TimeSpan(0, 0, 10));
        }
        private void addTrayIcon()
        {
            Icon icon = LuckDrawWindow.Properties.Resources.favicon;
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
            ContextMenu menu = new ContextMenu();

            //添加菜单的内容
            MenuItem WindowItem = new MenuItem();
            WindowItem.Text = "显示主页面";
            WindowItem.Click += new EventHandler(ShowMainWindow);

            MenuItem ExitItem = new MenuItem();
            ExitItem.Text = "退出";
            ExitItem.Click += new EventHandler(ExitApp);

            menu.MenuItems.Add(WindowItem);
            menu.MenuItems.Add(ExitItem);

            trayIcon.ContextMenu = menu;
        }
        private void removeTrayIcon()
        {
            if (trayIcon != null)
            {
                trayIcon.Visible = false;
                trayIcon.Dispose();
                trayIcon = null;
            }
        }
        private void ShowMainWindow(object sender, EventArgs e)
        {
            MainWindow.WindowState = WindowState.Normal;
            MainWindow.ShowInTaskbar = true;
        }
    }
}
