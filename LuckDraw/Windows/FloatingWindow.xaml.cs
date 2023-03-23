using PuranLai.Tools;
using System;
using System.Windows;
using System.Windows.Input;

namespace LuckDraw.Windows
{
    /// <summary>
    /// FloatingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FloatingWindow : Window
    {
        private bool isMouseIn;
        public FloatingWindow()
        {
            InitializeComponent();
            Left = SystemParameters.PrimaryScreenWidth - 350;
            Top = SystemParameters.PrimaryScreenHeight - 200;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Random random = new();
            GetButton.Content = random.Next(1, App.settings.number + 1);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (App.MainWindow is null)
                return;
            App.MainWindow.Visibility = Visibility.Visible;
            App.MainWindow.Activate();
        }

        private unsafe void Window_MouseEnter(object sender, MouseEventArgs e)   // Opacity Change Func
        {
            fixed (bool* MouseIn = &isMouseIn)
            {
                this.ChangeOpacity(128, MouseIn);
            }
        }

        private unsafe void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            fixed (bool* MouseIn = &isMouseIn)
            {
                this.ChangeOpacity(ExtendedWindowOps.OpacityOptions._1, MouseIn);
            }
        }
    }
}
