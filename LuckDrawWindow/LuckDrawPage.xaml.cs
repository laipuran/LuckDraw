using Microsoft.Toolkit.Uwp.Notifications;
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
using Windows.UI.Notifications;

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
        public class MyEx : Exception
        {
            public MyEx(string message) : base(message){ }
        }
        public void sendNotifications(string notifications)
        {
            ToastContent toastContent = new ToastContentBuilder()
       .AddToastActivationInfo("action=viewConversation&conversationId=5", ToastActivationType.Foreground)
       .AddText(notifications)
       .GetToastContent();
            var toast = new ToastNotification(toastContent.GetXml());
            DesktopNotificationManagerCompat.CreateToastNotifier().Show(toast);
        }
        private void GetNumberButton_Click(object sender, RoutedEventArgs e)
        {
            NumberTextBox.IsReadOnly = true;
            GetNumberButton.IsEnabled = false;

            int number;
            int max = App.numberOfPeople;
            try
            {
                number = int.Parse(NumberTextBox.Text);
                if (number <= 0)
                {
                    throw new MyEx("输入的数字不合法！");
                }
                else if (number>max)
                {
                    throw new MyEx("输入的数字超过总人数！");
                }
            }
            catch (Exception Ex)
            {
                ResultTextBlock.Text = Ex.Message;
                NumberTextBox.IsReadOnly = false;
                GetNumberButton.IsEnabled = true;
                return;
            }
            number = int.Parse(NumberTextBox.Text);

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
            while (chk == true)
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
                sendNotifications(notifications);
            }


            GetNumberButton.IsEnabled = true;
            NumberTextBox.IsReadOnly = false;
        }
    }
}
