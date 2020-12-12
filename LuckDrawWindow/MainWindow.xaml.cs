using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace LuckDrawWindow
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        bool MenuClosed = true;
        public MainWindow()
        {
            InitializeComponent();

            if (App.closeApp)
            {
                return;
            }

            DesktopNotificationManagerCompat.RegisterActivator<MyNotificationActivator>();
            DesktopNotificationManagerCompat.RegisterAumidAndComServer<MyNotificationActivator>("PuranLai.LuckDraw");

            if (App.closeApp)
            {
                Close();
            }

            App.numberOfPeople = Properties.Settings.Default.numberOfPeople;
            App.doShowToasts = Properties.Settings.Default.doShowToasts;

            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);

            if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {
                Title += "（管理员）";
            }

            App.FloatingWindow.Show();

            MiddleStackPanel.Width = 56;
            BackButton.Visibility = Visibility.Collapsed;
            LuckDrawListBoxItem.IsSelected = true;
            TitleTextBlock.Text = "抽奖";
        }
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            if (MenuClosed)
            {
                Storyboard openMenu = (Storyboard)HamburgerButton.FindResource("OpenMenu");
                openMenu.Begin();
                return;
            }
            Storyboard closeMenu = (Storyboard)HamburgerButton.FindResource("CloseMenu");
            closeMenu.Begin();

            MenuClosed = !MenuClosed;
        }

        private void ListBoxOfMainWindow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LuckDrawListBoxItem.IsSelected)
            {
                FrameOfMainWindow.NavigationService.Navigate(new Uri("LuckDrawPage.xaml", UriKind.Relative));
                BackButton.Visibility = Visibility.Collapsed;
                TitleTextBlock.Text = "抽奖";
                return;
            }
            if (RollListBoxItem.IsSelected)
            {
                FrameOfMainWindow.NavigationService.Navigate(new Uri("RollPage.xaml", UriKind.Relative));
                BackButton.Visibility = Visibility.Visible;
                TitleTextBlock.Text = "摇奖";
                return;
            }
            if (SettingsListBoxItem.IsSelected)
            {
                FrameOfMainWindow.NavigationService.Navigate(new Uri("SettingsPage.xaml", UriKind.Relative));
                BackButton.Visibility = Visibility.Visible;
                TitleTextBlock.Text = "设置";
                return;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (FrameOfMainWindow.CanGoBack)
            {
                FrameOfMainWindow.GoBack();
                if (FrameOfMainWindow.Source.ToString() == "LuckDrawPage.xaml")
                {
                    LuckDrawListBoxItem.IsSelected = true;
                    return;
                }
                if (FrameOfMainWindow.Source.ToString() == "RollPage.xaml")
                {
                    RollListBoxItem.IsSelected = true;
                    return;
                }
                if (FrameOfMainWindow.Source.ToString() == "SettingsPage.xaml")
                {
                    SettingsListBoxItem.IsSelected = true;
                    return;
                }
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            WindowState = WindowState.Minimized;
            ShowInTaskbar = false;
            e.Cancel = !App.closeApp;

            base.OnClosing(e);
        }
        protected override void OnClosed(EventArgs e)
        {
            Properties.Settings.Default.numberOfPeople = App.numberOfPeople;
            Properties.Settings.Default.doShowToasts = App.doShowToasts;
            Properties.Settings.Default.Save();

            App.FloatingWindow.Close();
            DesktopNotificationManagerCompat.History.Clear();

            if (App.trayIcon != null)
                App.trayIcon.Dispose();
            App.trayIcon = null;

            base.OnClosed(e);
        }
    }
}
