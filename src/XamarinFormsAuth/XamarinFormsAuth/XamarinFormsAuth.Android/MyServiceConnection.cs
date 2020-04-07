using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.CustomTabs;
using Android.Views;
using Android.Widget;

namespace XamarinFormsAuth.Droid
{
    public class MyServiceConnection : CustomTabsServiceConnection
    {
        private readonly Action<CustomTabsClient> customTabsServiceConnected;

        public MyServiceConnection(Action<CustomTabsClient> customTabsServiceConnected)
        {
            this.customTabsServiceConnected = customTabsServiceConnected;
        }

        public override void OnCustomTabsServiceConnected(ComponentName name, CustomTabsClient client)
        {
            customTabsServiceConnected?.Invoke(client);
        }

        public override void OnServiceDisconnected(ComponentName name)
        {

        }
    }
}