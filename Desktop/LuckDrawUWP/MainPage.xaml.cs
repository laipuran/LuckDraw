using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Data.Json;
using Windows.Storage;
using Windows.UI.Core.Preview;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Windows.UI.Xaml.Media.Imaging;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace LuckDraw
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        public MainPage()
        {
            this.InitializeComponent();

            if (!localSettings.Values.ContainsKey("numberOfPeople"))
            {
                localSettings.Values["numberOfPeople"] = 55;
            }

            if (!localSettings.Values.ContainsKey("doShowToasts"))
            {
                localSettings.Values["doShowToasts"] = false;
            }

            if (!localSettings.Values.ContainsKey("numbersLastTime"))
            {
                localSettings.Values["numbersLastTime"] = "欢迎使用抽奖！\n点击上侧的输入框然后抽取\n假如你喜欢它的话，进入 https://laipuran.github.io/about 打个赏吧~";
            }

            App.numberOfPeople = (int)localSettings.Values["numberOfPeople"];
            App.doShowToasts = (bool)localSettings.Values["doShowToasts"];
            FrameOfMainPage.Navigate(typeof(LuckDrawPage));
            TitleTextBlock.Text = "抽奖";

            GetBingWallPaper();
        }

        public void GetBingWallPaper()
        {
            var client = new WebClient();
            client.Encoding = Encoding.UTF8;
            var html = client.DownloadString("https://cn.bing.com/");
            var match = Regex.Match(html, "id=\"bgLink\".*?href=\"(.+?)\"");
            string url = string.Format("https://cn.bing.com{0}", match.Groups[1].Value);

            var filePath = Path.Combine(Path.GetTempPath(), "bing.jpg");
            client.DownloadFile(url, filePath);

            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri(filePath));
            Grid.Background = imageBrush;
        }
        private void ListBoxOfMainPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LuckDrawListBoxItem.IsSelected)
            {
                FrameOfMainPage.Navigate(typeof(LuckDrawPage));
                TitleTextBlock.Text = "抽奖";
                BackButton.Visibility = Visibility.Collapsed;
            }
            else if (RollListBoxItem.IsSelected)
            {
                FrameOfMainPage.Navigate(typeof(RollPage));
                TitleTextBlock.Text = "转盘";
                BackButton.Visibility = Visibility.Visible;
            }
            else if (SettingsListBoxItem.IsSelected)
            {
                FrameOfMainPage.Navigate(typeof(SettingsPage));
                TitleTextBlock.Text = "设置";
                BackButton.Visibility = Visibility.Visible;
            }
        }
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            SplitViewOfMainPage.IsPaneOpen = !SplitViewOfMainPage.IsPaneOpen;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (FrameOfMainPage.CanGoBack)
            {
                FrameOfMainPage.GoBack();
                LuckDrawListBoxItem.IsSelected = true;
            }
        }
    }
}
