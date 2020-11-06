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

            Top = 50;
            Left = 50;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void GetNumberButton_Click(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            ResultTextBlock.Text = r.Next(1, App.numberOfPeople + 1).ToString();
        }
    }
}
