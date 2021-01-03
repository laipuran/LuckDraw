using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Windows;
using System.Windows.Forms;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

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
            if (File.Exists(Path.GetTempPath()+".lock"))
            {
                IntPtr MainWindow = FindWindow("Luck Draw", null);
                if (MainWindow!=IntPtr.Zero)
                {
                    ShowWindowAsync(MainWindow, 1);
                }
                else
                {
                    MainWindow = FindWindow("Luck Draw（管理员）", null);
                    ShowWindowAsync(MainWindow, 1);
                }
                closeApp = true;
                return;
            }

            ShowSplashScreen();
            AddTrayIcon();

            string startupPath = GetType().Assembly.Location;
            var fileName = startupPath;
            var shortFileName = fileName.Substring(fileName.LastIndexOf('\\') + 1);
            var myReg = Registry.LocalMachine.OpenSubKey(
                "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", RegistryKeyPermissionCheck.ReadSubTree,
                RegistryRights.ReadKey);
            if (myReg == null || myReg.GetValue(shortFileName) == null)
            {
                System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
                System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
                //判断当前用户是否为管理员
                if (!principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
                {
                    //创建启动对象
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.UseShellExecute = true;
                    startInfo.WorkingDirectory = Environment.CurrentDirectory;
                    startInfo.FileName = fileName;
                    //设置启动动作,确保以管理员身份运行
                    startInfo.Verb = "runas";
                    System.Diagnostics.Process.Start(startInfo);
                    closeApp = true;
                }
                AutoStart();
            }

            File.Create(Path.GetTempFileName() + ".lock");
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            File.Delete(Directory.GetCurrentDirectory() + ".lock");

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
        private void AutoStart()
        {
            string startupPath = GetType().Assembly.Location;
            try
            {
                var fileName = startupPath;
                var shortFileName = fileName.Substring(fileName.LastIndexOf('\\') + 1);
                //打开子键节点
                var myReg = Registry.LocalMachine.OpenSubKey(
                    "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", RegistryKeyPermissionCheck.ReadSubTree,
                    RegistryRights.FullControl);
                if (myReg == null)
                {
                    //如果子键节点不存在，则创建之
                    myReg = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                }
                if (myReg != null && myReg.GetValue(shortFileName) == null)
                {
                    myReg.SetValue(shortFileName, fileName);
                }
                
            }
            catch(Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
    }
}
