using LuckDraw.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Image = System.Drawing.Image;

namespace LuckDraw
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool MenuClosed = true;
        Uri LuckDrawUri = new Uri("/Pages/LuckDrawPage.xaml", UriKind.Relative);
        Uri RollUri = new Uri("/Pages/RollPage.xaml", UriKind.Relative);
        Uri SettingsUri = new Uri("/Pages/SettingsPage.xaml", UriKind.Relative);
        ResourceDictionary Chinese = new ResourceDictionary();
        ResourceDictionary English = new ResourceDictionary();
        Languages currentLanguage = Languages.Chinese;
        FloatingWindow Floating = new();

        public MainWindow()
        {
            InitializeComponent();

            LuckDrawListBoxItem.IsSelected = true;
            TitleTextBlock.Text = GetString("LuckDraw");

            Icon = GetIcon("icon");
            LuckDrawContentImage.Source = GetIcon("LuckDraw");
            RollContentImage.Source = GetIcon("Roll");
            SettingsContentImage.Source = GetIcon("Settings");
            BackContentImage.Source = GetIcon("Back");
            MenuContentImage.Source = GetIcon("Menu");
            Floating.Show();
            App.mainwindow = this;
        }


        public enum Languages
        {
            Chinese,
            English
        }


        public static string GetString(string name)
        {
            return (string)Application.Current.FindResource(name);
        }

        private static ImageSource GetIcon(string name)
        {
            ResourceManager Loader = LuckDraw.Resources.Resource.ResourceManager;
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
#pragma warning disable CS8604 // 引用类型参数可能为 null。
            Bitmap icon = new((Image)Loader.GetObject(name));
#pragma warning restore CS8604 // 引用类型参数可能为 null。
#pragma warning restore CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
            return Imaging.CreateBitmapSourceFromHBitmap(icon.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        private void NavigationButton_Click(object sender, RoutedEventArgs e)
        {
            if (MenuClosed)
            {
                Storyboard openMenu = (Storyboard)FindResource("MenuOpen");
                openMenu.Begin();
            }
            else
            {
                Storyboard closeMenu = (Storyboard)FindResource("MenuClose");
                closeMenu.Begin();
            }
            MenuClosed = !MenuClosed;
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
    }
}
