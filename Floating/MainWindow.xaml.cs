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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Floating
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(string[] args)
        {
            InitializeComponent();

            Top = 50;
            Left = 50;
        }
        public MainWindow()
        {
            InitializeComponent();
            Top = 50;
            Left = 50;
        }
        
        private void GetNumberButton_Click(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            ResultTextBlock.Text = r.Next(1, App.numberOfPeople).ToString();
        }
        private void Window_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            if (Properties.Settings.Default.numberOfPeople != App.numberOfPeople)
            {
                Properties.Settings.Default.numberOfPeople = App.numberOfPeople;
            }
            base.OnClosing(e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ResultTextBlock.Text = App.numberOfPeople.ToString();
        }
    }
}
