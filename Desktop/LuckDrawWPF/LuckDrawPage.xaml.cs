using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Windows.UI.Notifications;

namespace LuckDraw
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
            public MyEx(string message) : base(message) { }
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
            Union union = Algorithm.Parser(NumberTextBox.Text, max);
            if (union.number == 0)
            {
                ResultTextBlock.Text = union.message;

                NumberTextBox.IsReadOnly = false;
                GetNumberButton.IsEnabled = true;
                return;
            }

            number = union.number;
            string output = Algorithm.Getter(number, max);

            Properties.Settings.Default.numbersLastTime = output;
            ResultTextBlock.Text = output;

            if (App.doShowToasts)
            {
                sendNotifications(output);
            }

            NumberTextBox.IsReadOnly = false;
            GetNumberButton.IsEnabled = true;
        }
    }
}
