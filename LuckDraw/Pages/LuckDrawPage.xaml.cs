using PuranLai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static PuranLai.Algorithm;

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
            ParsingResult result = ParseIntFromString(NumberComboBox.Text, App.settings.number);
            if (result.message != "")
            {
                MessageBox.Show(result.message + "\n\"" + NumberComboBox.Text + "\"");
                return;
            }

            ResultTextBlock.Text = GetRandomResult(result.number, App.settings.number, '\n', MainWindow.GetString("TipGot"));
        }
    }
}
