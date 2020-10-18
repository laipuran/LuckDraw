using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Floating
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        internal static int numberOfPeople;
    }

    class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length>0)
            {
                App.numberOfPeople = int.Parse(String.Join("", args));
                Properties.Settings.Default.numberOfPeople = App.numberOfPeople;
                Properties.Settings.Default.Save();
            }
            else
            {
                App.numberOfPeople = Properties.Settings.Default.numberOfPeople;
            }

            Floating.App app = new Floating.App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
