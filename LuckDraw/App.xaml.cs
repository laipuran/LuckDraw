using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LuckDraw
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary
    public partial class App : Application
    {
        internal static Settings settings = new(55);
        internal static MainWindow mainwindow = null;
    }
}
