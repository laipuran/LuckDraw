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
                try
                {
                    int number = int.Parse(NumberTextBox.Text);
                    if (number < 0)
                    {
                        throw new LuckDrawPage.MyEx("输入的数字不合法！");
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    NumberTextBox.Text = App.numberOfPeople.ToString();
                    return;
                }
                App.numberOfPeople = int.Parse(NumberTextBox.Text);
            }
            else
            {
                NumberTextBox.Text = App.numberOfPeople.ToString();
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
