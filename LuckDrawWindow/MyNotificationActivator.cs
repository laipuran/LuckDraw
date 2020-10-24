// The GUID CLSID must be unique to your app. Create a new GUID if copying this code.
using LuckDrawWindow;
using Microsoft.QueryStringDotNET;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Runtime.InteropServices;
using System.Windows;
using static Microsoft.Toolkit.Uwp.Notifications.NotificationActivator;

[ClassInterface(ClassInterfaceType.None)]
[ComSourceInterfaces(typeof(INotificationActivationCallback))]
[Guid("5C5BC8EA-4B17-7315-FF09-899091168E2C"), ComVisible(true)]

public class MyNotificationActivator : NotificationActivator
{
    public override void OnActivated(string invokedArgs, NotificationUserInput userInput, string appUserModelId)
    {

    }
}
