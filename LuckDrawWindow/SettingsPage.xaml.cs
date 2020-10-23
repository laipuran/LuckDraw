using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace LuckDrawWindow
{
    /// <summary>
    /// SettingsPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();

            NumberTextBox.Text = App.numberOfPeople.ToString();
            ToastToggleButton.IsChecked = App.doShowToasts;
            if ((bool)ToastToggleButton.IsChecked)
            {
                ToastToggleButton.Content = "打开";
            }
            else if (!(bool)ToastToggleButton.IsChecked)
            {
                ToastToggleButton.Content = "关闭";
            }
        }
        public bool DownloadFile(string strFileName, string file)
        {
            bool flag = false;
            //打开上次下载的文件
            long SPosition = 0;
            //实例化流对象
            FileStream FStream;
            //文件不保存创建一个文件
            File.Delete(Directory.GetCurrentDirectory().ToString() + file);
            FStream = new FileStream(strFileName, FileMode.Create);
            SPosition = 0;
            try
            {
                //打开网络连接
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://laipuran.github.io/LuckDraw/" + file);
                if (SPosition > 0)
                    myRequest.AddRange((int)SPosition);             //设置Range值
                //向服务器请求,获得服务器的回应数据流
                Stream myStream = myRequest.GetResponse().GetResponseStream();
                //定义一个字节数据
                byte[] btContent = new byte[512];
                int intSize = 0;
                intSize = myStream.Read(btContent, 0, 512);
                while (intSize > 0)
                {
                    FStream.Write(btContent, 0, intSize);
                    intSize = myStream.Read(btContent, 0, 512);
                }
                //关闭流
                FStream.Close();
                myStream.Close();
                flag = true;        //返回true下载成功
            }
            catch (Exception Ex)
            {
                FStream.Close();
                MessageBox.Show(Ex.Message);
                flag = false;       //返回false下载失败
            }
            return flag;
        }
        private void NumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NumberTextBox.Text != null)
            {
                try
                {
                    int number = int.Parse(NumberTextBox.Text);
                    if (number < 0)
                    {
                        throw new LuckDrawPage.MyEx("输入的数字不合法！");
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    NumberTextBox.Text = App.numberOfPeople.ToString();
                    return;
                }
                App.numberOfPeople = int.Parse(NumberTextBox.Text);
            }
            else
            {
                NumberTextBox.Text = App.numberOfPeople.ToString();
            }
        }

        private void ToastToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)ToastToggleButton.IsChecked)
            {
                ToastToggleButton.Content = "打开";
            }
            else if (!(bool)ToastToggleButton.IsChecked)
            {
                ToastToggleButton.Content = "关闭";
            }
            App.doShowToasts = (bool)ToastToggleButton.IsChecked;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool flag = DownloadFile("version.txt", "version.txt");
            if (!flag)
            {
                MessageBox.Show("下载失败！", "下载结果");
                return;
            }
            string version = File.ReadAllText(Directory.GetCurrentDirectory().ToString() + "\\version.txt");
            MessageBox.Show("最新版本：v" + version);
            float ver = Convet.ToSingle(ver);
            if (ver > Properties.Settings.Defalt.CurrenVersion)
            {
                bool isSuccessfully = DownloadFile("assets/LuckDrawSetup.msi", "Setup.msi");
                if (!flag)
                {
                    MessageBox.Show("下载成功！");
                    Process.Start(Directory.GetCurrentDirectory().ToString() + "Setup.msi");
                    return;
                }
                else
                {
                    MessageBox.Show("下载失败！");
                    return;
                }
            }
        }
    }
}
