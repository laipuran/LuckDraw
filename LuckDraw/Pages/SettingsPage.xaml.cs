using PuranLai.Algorithms;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static LuckDraw.Language;

namespace LuckDraw.Pages
{
    /// <summary>
    /// SettingsPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
            TotalNumberTextBox.Text = App.settings.number.ToString();
        }

        private void TotalNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ParsingResult result = Parse.ParseFromString(TotalNumberTextBox.Text, 100000);
            if (result.message == "")
            {
                App.settings.number = result.number;
                return;
            }
            TotalNumberTextBox.Text = App.settings.number.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (App.settings.lang == Languages.Chinese)
            {
                SetEnglish();
            }
            else
            {
                SetChinese();
            }
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://laipuran.github.io/LuckDraw");
        }
    }
}
