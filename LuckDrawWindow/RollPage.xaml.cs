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

namespace LuckDrawWindow
{
    /// <summary>
    /// RollPage.xaml 的交互逻辑
    /// </summary>
    public partial class RollPage : Page, INotifyPropertyChanged
    {
        public RollPage()
        {
            InitializeComponent();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private Random random = new Random();

        private Boolean isRolling = false;

        public Boolean IsRolling
        {
            get
            {
                return isRolling;
            }
            set
            {
                isRolling = value;
                PropertyChanged(this, new PropertyChangedEventArgs("RollButtonText"));
            }
        }

        public string RollButtonText
        {
            get
            {
                if (isRolling)
                {
                    return "Stop rolling";
                }
                else
                {
                    return "Start rolling";
                }
            }
        }

        private int endNumber = 99;

        public int EndNumber
        {
            get
            {
                return endNumber;
            }
            set
            {
                endNumber = value;
            }
        }

        private int randomNumber;

        public int RandomNumber
        {
            get
            {
                return randomNumber;
            }
            set
            {
                randomNumber = value;
                PropertyChanged(this, new PropertyChangedEventArgs("RandomNumber"));
            }
        }


        private void ToggleRoll(object sender, RoutedEventArgs args)
        {
            IsRolling = !IsRolling;
            Task.Run(Rolling);
        }

        private async void Rolling()
        {
            while (isRolling)
            {
                RandomNumber = random.Next(1, endNumber + 1);
                await Task.Delay(100);
            }
        }
    }
}
