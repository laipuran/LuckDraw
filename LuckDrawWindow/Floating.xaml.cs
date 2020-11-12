using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

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
            Pop.IsOpen = true;
        }

        private void SeewoBoardButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DesktopButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainWindowButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
