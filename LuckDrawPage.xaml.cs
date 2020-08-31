using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.Toolkit.Uwp.Notifications; // Notifications library
using Microsoft.QueryStringDotNET; // QueryString.NET
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using Windows.UI.Notifications;

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
        private int getRand(int a, int b)
        {
            Random r = new Random();
            int num = r.Next(a, b);
            return num;
        }
        private bool Current_Activated(object sender, WindowActivatedEventArgs e)
        {
            bool visibility = e.WindowActivationState != CoreWindowActivationState.Deactivated;
            return visibility;
        }
        public void sendNotifications(string title, string notifications, int waitSeconds)
        {
            ToastVisual visual = new ToastVisual()
            {
                BindingGeneric = new ToastBindingGeneric()
                {
                    Children =
                    {
                        new AdaptiveText()
                        {
                            Text = title,
                            HintMaxLines = 1
                        },

                        new AdaptiveText()
                        {
                            Text = notifications
                        },

                        new AdaptiveText()
                        {
                            Text = "来自：Luck Draw"
                        }
                    }
                }
            };
            ToastContent toastContent = new ToastContent()
            {
                Visual = visual,
            };
            var toast = new ToastNotification(toastContent.GetXml());
            toast.ExpirationTime = DateTime.Now.AddSeconds(waitSeconds);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    private void GetNumber_Click(object sender, RoutedEventArgs e)
        {
            NumberComboBox.IsEnabled = false;
            GetNumberButton.IsEnabled = false;

            int number = int.Parse(NumberComboBox.Text);
            if (number<=0)
            {
                ResultTextBlock.Text = "输入值非法！";
                GetNumberButton.IsEnabled = true;
                NumberComboBox.IsEnabled = true;
                return;
            }
            int max = App.numberOfPeople > 1 ? App.numberOfPeople : 55;

            int[] array=new int[number];
            for (int i = 0; i < number; i++)
            {
                array[i] = getRand(1, max+1);
            }

            string output, notifications;
            output = "被抽中的幸运同学：\n"+string.Join("\n", array);
            notifications = "被抽中的幸运同学： " + string.Join(" ", array);
            ResultTextBlock.Text = output;

            sendNotifications("请查看抽取的学号名单", notifications, 5);

            GetNumberButton.IsEnabled = true;
            NumberComboBox.IsEnabled = true;
        }
    }
}
