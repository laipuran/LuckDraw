using System;
using System.Drawing;
using System.Runtime.InteropServices;
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
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "ShowWindowAsync", SetLastError = true)]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        internal static int numberOfPeople;
        internal static bool doShowToasts;
        internal static bool closeApp = false;
        internal static Window FloatingWindow = new Floating();
        internal static NotifyIcon trayIcon;

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
        public void ExitApp(object sender, EventArgs e)
        {
            closeApp = true;
            MainWindow.Close();
        }
        private void ShowSplashScreen()
        {
            SplashScreen s = new SplashScreen("SplashScreen.png");
            s.Show(true);
        }
        private void AddTrayIcon()
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
        private void RemoveTrayIcon()
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
