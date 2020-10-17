using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floating
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            App app = new App();
            MainWindow window = new MainWindow(args);
            app.MainWindow = window;
            app.InitializeComponent();
            app.Run();
        }
    }
}
