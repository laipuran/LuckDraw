using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LuckDrawXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LuckDrawPage : ContentPage
    {
        public LuckDrawPage()
        {
            InitializeComponent();
            Title = "抽奖";
        }

        public class MyEx : Exception
        {
            public MyEx(string message) : base(message) { }
        }
        private void GetButton_Clicked(object sender, EventArgs e)
        {
            NumberEditor.IsReadOnly = true;
            GetButton.IsEnabled = false;

            int number;
            int max = (int)Application.Current.Properties["number"];
            try
            {
                number = int.Parse(NumberEditor.Text);
                if (number <= 0)
                {
                    throw new MyEx("输入的数字不合法！");
                }
                if (number > max)
                {
                    throw new MyEx("输入的数字超过总人数！");
                }
            }
            catch (Exception Ex)
            {
                ResultLabel.Text = Ex.Message;
                NumberEditor.IsReadOnly = false;
                GetButton.IsEnabled = true;
                return;
            }
            number = int.Parse(NumberEditor.Text);


            int[] array = new int[number];
            int[] check = new int[max];
            Array.Clear(array, 0, number);
            Array.Clear(check, 0, max);

            Random r = new Random();
            for (int i = 0; i < number; i++)
            {
                int temp = r.Next(1, max + 1);
                check[temp - 1]++;
                Thread.Sleep(10);
            }

            bool chk = true;
            while (chk == true)                         // Check if the numbers are the same
            {
                for (int i = 0; i < max; i++)
                {
                    if (check[i] > 1)
                    {
                        check[i]--;
                        int temp = r.Next(1, max + 1);
                        check[temp - 1]++;
                    }
                }
                int index = 0;
                for (int i = 0; i < max; i++)
                {
                    if (check[i] == 1 || check[i] == 0)
                    {
                        if (check[i] == 1)
                        {
                            array[index] = i + 1;
                            index++;
                        }
                        if (i == max - 1)
                        {
                            chk = false;
                        }
                    }
                    else break;
                }

            }

            for (int i = number - 1; i > 0; i--)        // Sort
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
            ResultLabel.Text = output;

            GetButton.IsEnabled = true;
            NumberEditor.IsReadOnly = false;
        }
    }
}