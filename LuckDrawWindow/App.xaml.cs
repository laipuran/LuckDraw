using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LuckDrawWindow
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        internal static int numberOfPeople;
        internal static bool doShowToasts;
        protected override void OnStartup(StartupEventArgs e)
        {
            SplashScreen s = new SplashScreen("SplashScreen.png");
            s.Show(true);
            s.Close(new TimeSpan(0, 0, 10));
            base.OnStartup(e);
        }
    }
}
