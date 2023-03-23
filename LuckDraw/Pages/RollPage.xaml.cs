using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LuckDraw.Pages
{
    /// <summary>
    /// Roll.xaml 的交互逻辑
    /// </summary>
    public partial class RollPage : Page
    {
        public RollPage()
        {
            InitializeComponent();
        }

        private void RollButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => Dispatcher.BeginInvoke(new Action(() => { Roll(); })));
        }

        private async void Roll()
        {
            Random random = new();
            int num = random.Next(1, App.settings.number + 1);
            string tipGot = MainWindow.GetString("TipGot");
            RollButton.Content = MainWindow.GetString("Stop");
            while (RollButton.IsChecked == true)
            {
                await Task.Run(() => Dispatcher.BeginInvoke(new Action(() =>
                {
                    random = new(num);
                    num = random.Next(1, App.settings.number + 1);
                    ResultTextBox.Text = tipGot + num;
                    Task.Delay(50);
                })));
            }
            RollButton.Content = MainWindow.GetString("Start");
        }
    }
}
