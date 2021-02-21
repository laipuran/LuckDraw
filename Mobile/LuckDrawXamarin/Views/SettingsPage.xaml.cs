using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static LuckDrawXamarin.Views.LuckDrawPage;

namespace LuckDrawXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            Title = "设置";
        }
        private void NumberEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NumberEditor.Text == "") {
                Title = "设置";
                return;
            }
            int numberOfPeople;
            try
            {
                numberOfPeople = int.Parse(NumberEditor.Text);
                if (numberOfPeople <= 0)
                {
                    throw new MyEx("输入的数字不合法！");
                }
            }
            catch (Exception Ex)
            {
                Title = Ex.Message;
                return;
            }
            Application.Current.Properties["number"] = int.Parse(NumberEditor.Text);
            Title = "设置";
        }
        protected override void OnAppearing()
        {
            NumberEditor.Text = Application.Current.Properties["number"].ToString();
            base.OnAppearing();
        }
    }
}