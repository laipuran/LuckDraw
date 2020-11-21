using System;
using System.Windows;
using System.Windows.Input;

namespace LuckDrawWindow
{
    /// <summary>
    /// Floating.xaml 的交互逻辑
    /// </summary>
    public partial class Floating : Window
    {
        public Floating()
        {
            InitializeComponent();

            Top = 100;
            Left = 50;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void GetNumberButton_Click(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            ResultTextBlock.Text = r.Next(1, App.numberOfPeople + 1).ToString();
        }

        private void GetNumberButton_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pop.IsOpen = !Pop.IsOpen;
        }
        private void Pop_LostFocus(object sender, RoutedEventArgs e)
        {
            Pop.IsOpen = false;
        }
        private void DesktopButton_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
