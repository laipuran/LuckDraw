using System;
using System.Drawing;
using System.Resources;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static LuckDraw.Language;
using Image = System.Drawing.Image;

namespace LuckDraw
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool MenuClosed = true;
        readonly Uri LuckDrawUri = new("/Pages/LuckDrawPage.xaml", UriKind.Relative);
        readonly Uri RollUri = new("/Pages/RollPage.xaml", UriKind.Relative);
        readonly Uri SettingsUri = new("/Pages/SettingsPage.xaml", UriKind.Relative);

        public MainWindow()
        {
            InitializeComponent();

            App.ContentFrame = ContentFrame;
            App.MainWindow = this;

            if (App.settings.lang == Languages.Chinese)
            {
                SetChinese();
            }
            else
                SetEnglish();

            LuckDrawListBoxItem.IsSelected = true;
            TitleTextBlock.Text = GetString("LuckDraw");
        }

        public static string GetString(string name)
        {
            return (string)Application.Current.FindResource(name);
        }

        private void NavigationButton_Click(object sender, RoutedEventArgs e)
        {
            if (MenuClosed)
            {
                Task.Run(() => Dispatcher.BeginInvoke(new Action(() => { MenuOpen(); })));
            }
            else
            {
                Task.Run(() => Dispatcher.BeginInvoke(new Action(() => { MenuClose(); })));
            }
            MenuClosed = !MenuClosed;
        }

        private async void MenuOpen()
        {
            DateTime start = DateTime.Now;
            TimeSpan span = new();
            double k = (170 - 45) / Math.Sin(GetX(200, 150));
            while (span.TotalMilliseconds < 200)
            {
                span = DateTime.Now - start;
                await Task.Run(() => Dispatcher.BeginInvoke(new Action(() =>
                {
                    double x = span.TotalMilliseconds;
                    double value = Math.Sin(GetX(x, 150)) * k + 45;
                    MenuStackPanel.Width = value;
                })));
            }
            MenuStackPanel.Width = 170;
        }

        private async void MenuClose()
        {
            DateTime start = DateTime.Now;
            TimeSpan span = new();
            double k = (170 - 45) / Math.Sin(GetX(200, 150));
            while (span.TotalMilliseconds < 200)
            {
                span = DateTime.Now - start;
                await Task.Run(() => Dispatcher.BeginInvoke(new Action(() =>
                {
                    double x = span.TotalMilliseconds;
                    double value = 170 - Math.Sin(GetX(x, 150)) * k;
                    MenuStackPanel.Width = value;
                })));
            }
            MenuStackPanel.Width = 45;
        }

        private static double GetX(double time, int T)
        {
            double x = Math.PI * time / T / 2;

            return x;
        }

        private void ContentListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ContentListBox.SelectedItem == LuckDrawListBoxItem)
            {
                ContentFrame.NavigationService.Navigate(LuckDrawUri);
            }
            else if (ContentListBox.SelectedItem == RollListBoxItem)
            {
                ContentFrame.NavigationService.Navigate(RollUri);
            }
            else if (ContentListBox.SelectedItem == SettingsListBoxItem)
            {
                ContentFrame.NavigationService.Navigate(SettingsUri);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ContentFrame.CanGoBack)
            {
                return;
            }
            ContentFrame.GoBack();
        }

        private void ContentFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if ("/" + ContentFrame.Source.ToString() == LuckDrawUri.ToString())
            {
                TitleTextBlock.Text = GetString("LuckDraw");
                LuckDrawListBoxItem.IsSelected = true;
            }
            else if ("/" + ContentFrame.Source.ToString() == RollUri.ToString())
            {
                TitleTextBlock.Text = GetString("Roll");
                RollListBoxItem.IsSelected = true;
            }
            else if ("/" + ContentFrame.Source.ToString() == SettingsUri.ToString())
            {
                TitleTextBlock.Text = GetString("Settings");
                SettingsListBoxItem.IsSelected = true;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                this.Visibility = Visibility.Collapsed;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Settings.Save(App.settings);
            App.Current.Shutdown();
        }
    }
}
