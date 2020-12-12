using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace LuckDrawWindow
{
    /// <summary>
    /// Floating.xaml 的交互逻辑
    /// </summary>
    public partial class Floating : Window
    {
        private delegate bool WndEnumProc(IntPtr hWnd, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool EnumWindows(WndEnumProc lpEnumFunc, int lParam);
        [DllImport("user32")]
        private static extern IntPtr GetParent(IntPtr hWnd);
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "ShowWindowAsync", SetLastError = true)]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("user32.dll", EntryPoint = "IsWindowVisible", SetLastError = true)]
        public static extern bool IsWindowVisible(IntPtr hWnd);
        public Floating()
        {
            InitializeComponent();

            double ScreenHeight = SystemParameters.PrimaryScreenHeight;
            double ScreenWidth = SystemParameters.PrimaryScreenWidth;

            Left = ScreenWidth - 120;
            Top = ScreenHeight - 150;

            GetNumberButton.Focus();
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
            EnumWindows(OnWindowEnum, 0);
            bool OnWindowEnum(IntPtr hWnd, int lParam)
            {
                if (GetParent(hWnd) == IntPtr.Zero && IsWindowVisible(hWnd) != false)
                {
                    ShowWindowAsync(hWnd, 6);
                }
                return true;
            }
            IntPtr handle = FindWindow(null, "Floating");
            ShowWindowAsync(handle, 1);

        }
        private void MainWindowButton_Click(object sender, RoutedEventArgs e)
        {
            IntPtr mainWindow = FindWindow(null, "Luck Draw");
            if (mainWindow != IntPtr.Zero)
            {
                ShowWindowAsync(mainWindow, 1);
                return;
            }
            mainWindow = FindWindow(null, "Luck Draw（管理员）");
            ShowWindowAsync(mainWindow, 1);
        }

        private void SeewoWhiteBoardButton_Click(object sender, RoutedEventArgs e)
        {
            IntPtr seewoWhiteBoard = FindWindow(null, "希沃白板 5");
            if (seewoWhiteBoard != IntPtr.Zero)
            {
                ShowWindowAsync(seewoWhiteBoard, 1);
                return;
            }
            seewoWhiteBoard = FindWindow(null, "希沃白板 3");
            ShowWindowAsync(seewoWhiteBoard, 1);
        }
    }
}
