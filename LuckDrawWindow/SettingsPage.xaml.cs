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

namespace LuckDrawWindow
{
    /// <summary>
    /// SettingsPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();

            NumberTextBox.Text = App.numberOfPeople.ToString();
            ToastToggleButton.IsChecked = App.doShowToasts;
            if ((bool)ToastToggleButton.IsChecked)
            {
                ToastToggleButton.Content = "打开";
            }
            else if (!(bool)ToastToggleButton.IsChecked)
            {
                ToastToggleButton.Content = "关闭";
            }
        }

        private void NumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NumberTextBox.Text != null)
            {
                App.numberOfPeople = int.Parse(NumberTextBox.Text);
            }
        }

        private void ToastToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)ToastToggleButton.IsChecked)
            {
                ToastToggleButton.Content = "打开";
            }
            else if (!(bool)ToastToggleButton.IsChecked)
            {
                ToastToggleButton.Content = "关闭";
            }
            App.doShowToasts = (bool)ToastToggleButton.IsChecked;
        }
    }
}
