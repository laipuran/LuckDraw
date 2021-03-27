using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace LuckDraw
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool MenuClosed = true;

        public MainWindow()
        {
            InitializeComponent();

            if (App.closeApp)
            {
                Close();
                return;
            }
            GetBingWallPaper();
            App.numberOfPeople = Properties.Settings.Default.numberOfPeople;

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
                MenuClosed = false;
                return;
            }
            else
            {
                Storyboard closeMenu = (Storyboard)HamburgerButton.FindResource("CloseMenu");
                closeMenu.Begin();
                MenuClosed = true;
                return;
            }
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
        public void GetBingWallPaper()
        {
            var client = new WebClient
            {
                Encoding = Encoding.UTF8
            };
            var html = client.DownloadString("https://cn.bing.com/");
            var match = Regex.Match(html, "id=\"bgLink\".*?href=\"(.+?)\"");
            string url = string.Format("https://cn.bing.com{0}", match.Groups[1].Value);

            var filePath = Path.Combine(Path.GetTempPath(), "bing.jpg");
            try
            {
                client.DownloadFile(url, filePath);
            }
            catch
            {
                filePath += "1";
                client.DownloadFile(url, filePath);
            }
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(filePath));
            brush.Opacity = 0.5;
            brush.Stretch = Stretch.UniformToFill;
            Background = brush;
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
            Properties.Settings.Default.Save();

            App.FloatingWindow.Close();

            base.OnClosed(e);
        }
    }
}
