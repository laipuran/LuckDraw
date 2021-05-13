using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板
// From Litrop: https://github.com/Litrop/roll

namespace LuckDraw
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public partial class RollPage
    {
        public RollPage()
        {
            InitializeComponent();
        }
        private bool isRolling = false;
        int max = App.numberOfPeople;
        Random r = new Random();
        private void GetNumberButton_Click(object sender, RoutedEventArgs e)
        {
            isRolling = !isRolling;
            if (isRolling)
            {
                GetNumberButton.Content = "停止";
                Task.Run(Roll);
            }
            GetNumberButton.Content = "开始";
        }
        private async void Roll()
        {
            while (isRolling)
            {
                int num = r.Next(1, max + 1);
                NumberTextBlock.Text = num.ToString();
                await Task.Delay(500);
            }
        }
    }
}
