using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            int[] check = new int[max];
            Array.Clear(array, 0, number);
            Array.Clear(check, 0, max);

            Random r = new Random();
            for (int i = 0; i < number; i++)
            {
                int temp = r.Next(1, max + 1);
                check[temp]++;
                Thread.Sleep(10);
            }

            bool chk = true;
            while (chk == true)
            {
                for (int i = 0; i < max; i++)
                {
                    if (check[i] > 1)
                    {
                        check[i]--;
                        int temp = r.Next(1, max + 1);
                        check[temp]++;
                    }
                }
                for (int i = 0; i < max; i++)
                {
                    if (check[i] == 1 || check[i] == 0)
                    {
                        if (i == max - 1)
                        {
                            chk = false;
                        }
                    }
                    else break;
                }

            }

            {
                int temp = 0;
                for (int i = 0; i < max; i++)
                {
                    if (check[i] == 1)
                    {
                        array[temp] = i + 1;
                        temp++;
                    }
                }
            }

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
