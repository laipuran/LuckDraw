using LuckDrawXamarin.Droid.DependencyService;
using System;
[assembly: Xamarin.Forms.Dependency(typeof(AppHandlerImplementation))]

namespace LuckDrawXamarin.Droid.DependencyService
{
    public class AppHandlerImplementation : IAppHandler

    {
        public AppHandlerImplementation()
        {
        }
        [Obsolete]
        public void ShowToastMessage(string strMessage, bool bLong = false)
        {
            Android.Widget.Toast.MakeText(Xamarin.Forms.Forms.Context, strMessage, bLong ? Android.Widget.ToastLength.Long : Android.Widget.ToastLength.Short).Show();
        }
    }
}