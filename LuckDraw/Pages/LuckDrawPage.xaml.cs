using PuranLai.Algorithms;
using System.Windows;
using System.Windows.Controls;

namespace LuckDraw.Pages
{
    /// <summary>
    /// LuckDrawPage.xaml 的交互逻辑
    /// </summary>
    public partial class LuckDrawPage : Page
    {
        public LuckDrawPage()
        {
            InitializeComponent();
        }

        private void GetNumber_Click(object sender, RoutedEventArgs e)
        {
            ParsingResult result = Parse.ParseFromString(NumberComboBox.Text, App.settings.number);
            if (result.message != "")
            {
                MessageBox.Show(result.message + "\n\"" + NumberComboBox.Text + "\"");
                return;
            }

            ResultTextBlock.Text = RandomNumber.GetRandomResult(result.number, App.settings.number, '\n', MainWindow.GetString("TipGot"));
        }
    }
}
