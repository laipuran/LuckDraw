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
    /// LuckDrawPage.xaml 的交互逻辑
    /// </summary>
    public partial class LuckDrawPage : Page
    {
        public LuckDrawPage()
        {
            InitializeComponent();

            ResultTextBlock.Text = Properties.Settings.Default.numbersLastTime;
        }
        static public int getRand(int a, int b)
        {
            Random r = new Random();
            int num = r.Next(a, b);
            return num;
        }

        private void GetNumberButton_Click(object sender, RoutedEventArgs e)
        {

            NumberTextBox.IsEnabled = false;
            GetNumberButton.IsEnabled = false;

            int number = int.Parse(NumberTextBox.Text);
            if (number <= 0)
            {
                ResultTextBlock.Text = "输入值非法！";
                GetNumberButton.IsEnabled = true;
                NumberTextBox.IsEnabled = true;
                return;
            }
            int max = App.numberOfPeople;

            int[] array = new int[number];
            for (int i = 0; i < number; i++)
            {
                array[i] = getRand(1, max + 1);
            }

            int[] rankedNumbers = new int[number];
            bool flag = true;
            for (int i = 0; i < number - 1; i++)
            {
                if (array[i] < array[i + 1])
                {
                    continue;
                }
                else
                {
                    flag = false;
                    break;
                }
            }
            if (!flag)
            {
                for (int i = number - 1; i > 0; i--)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (array[j] > array[j + 1])
                        {
                            int temp;
                            temp = array[j + 1];
                            array[j + 1] = array[j];
                            array[j] = temp;
                        }
                    }
                }
            }

            string output;
            output = "被抽中的幸运同学：\n" + string.Join("\n", array);
            Properties.Settings.Default.numbersLastTime = output;
            ResultTextBlock.Text = output;

            if (App.doShowToasts)
            {
                string notifications;
                notifications = "被抽中的幸运同学： " + string.Join(" ", array);
            }


            GetNumberButton.IsEnabled = true;
            NumberTextBox.IsEnabled = true;
        }
    }
}
