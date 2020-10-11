using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            App.numberOfPeople = Properties.Settings.Default.numberOfPeople;
            App.doShowToasts = Properties.Settings.Default.doShowToasts;

            string path = System.IO.Directory.GetCurrentDirectory() + "\\Floating\\Floating.exe";
            if (File.Exists(path))
            {
                Process.Start(path);
            }

            Storyboard closeMenu = (Storyboard)HamburgerButton.FindResource("CloseMenu");
            closeMenu.Begin();
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
            }
            else
            {
                Storyboard closeMenu = (Storyboard)HamburgerButton.FindResource("CloseMenu");
                closeMenu.Begin();
            }

            MenuClosed = !MenuClosed;
        }

        private void ListBoxOfMainWindow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LuckDrawListBoxItem.IsSelected)
            {
                FrameOfMainWindow.NavigationService.Navigate(new Uri("LuckDrawPage.xaml", UriKind.Relative));
                BackButton.Visibility = Visibility.Collapsed;
                TitleTextBlock.Text = "抽奖";
            }
            else if (RollListBoxItem.IsSelected)
            {
                FrameOfMainWindow.NavigationService.Navigate(new Uri("RollPage.xaml", UriKind.Relative));
                BackButton.Visibility = Visibility.Visible;
                TitleTextBlock.Text = "摇奖";
            }
            else if (SettingsListBoxItem.IsSelected)
            {
                FrameOfMainWindow.NavigationService.Navigate(new Uri("SettingsPage.xaml", UriKind.Relative));
                BackButton.Visibility = Visibility.Visible;
                TitleTextBlock.Text = "设置";
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
                }
                else if (FrameOfMainWindow.Source.ToString() == "RollPage.xaml")
                {
                    RollListBoxItem.IsSelected = true;
                }
                else if (FrameOfMainWindow.Source.ToString() == "SettingsPage.xaml")
                {
                    SettingsListBoxItem.IsSelected = true;
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Properties.Settings.Default.numberOfPeople = App.numberOfPeople;
            Properties.Settings.Default.doShowToasts = App.doShowToasts;
            Properties.Settings.Default.Save();
            base.OnClosing(e);
        }
    }
}
