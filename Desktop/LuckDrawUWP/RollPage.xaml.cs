using System;
using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板
// From Litrop: https://github.com/Litrop/roll

namespace LuckDraw
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class RollPage : Page
    {
        public RollPage()
        {
            this.InitializeComponent();
        }

        static public int getRand(int a, int b)
        {
            Random r = new Random();
            int num = r.Next(a, b);
            return num;
        }
        private async void startRolling(object sender, RoutedEventArgs e)
        {
            int max = App.numberOfPeople > 1 ? App.numberOfPeople : 55;
            Random r = new Random();
            while (true)
            {
                int num = r.Next(1, max + 1);
                string number = num.ToString();
                NumberTextBlock.Text = number;
                await System.Threading.Tasks.Task.Delay(100);
            }
        }
        private void GetNumberButton_Click(object sender, RoutedEventArgs e)
        {
            var thread1 = new Thread(() =>
              {
                  int max = App.numberOfPeople > 1 ? App.numberOfPeople : 55;
                  Random r = new Random();
                  int num = r.Next(1, max + 1);
                  string number = num.ToString();
                  NumberTextBlock.Text = number;
                  Thread.Sleep(100);
              });
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
