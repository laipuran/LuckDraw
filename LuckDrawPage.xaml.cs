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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace LuckDraw
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class LuckDrawPage : Page
    {
        public LuckDrawPage()
        {
            this.InitializeComponent();
        }
        int getRand(int a, int b)
        {
            Random r = new Random();
            int num = r.Next(a, b);
            return num;
        }
        private void GetNumber_Click(object sender, RoutedEventArgs e)
        {
            NumberTextBox.IsReadOnly = true;
            GetNumberButton.IsEnabled = false;

            int number = int.Parse(NumberTextBox.Text);
            if (number<=0)
            {
                ResultTextBlock.Text = "输入值非法！";
                GetNumberButton.IsEnabled = true;
                NumberTextBox.IsReadOnly = false;
                return;
            }
            int max = App.numberOfPeople <= 1 ? App.numberOfPeople : 55;

            int[] array=new int[number];
            for (int i = 0; i < number; i++)
            {
                array[i] = getRand(1, max+1);
            }
            string output;
            output = "被抽中的幸运同学：\n"+string.Join("\n", array);
            ResultTextBlock.Text = output;

            GetNumberButton.IsEnabled = true;
            NumberTextBox.IsReadOnly = false;
        }
    }
}
