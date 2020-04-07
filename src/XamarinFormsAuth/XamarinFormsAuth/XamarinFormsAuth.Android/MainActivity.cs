using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.CustomTabs;
using Android.Support.CustomTabs.Chromium.SharedUtilities;
using Android.Content;
using System.Collections.Generic;
using System.Linq;
using Plugin.CurrentActivity;

namespace XamarinFormsAuth.Droid
{
    [Activity(Label = "XamarinFormsAuth", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private readonly string SERVICE_ACTION = "android.support.customtabs.action.CustomTabsService";
        private readonly string CHROME_PACKAGE = "com.android.chrome";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            LoadApplication(new App());

            
            

            Intent serviceIntent = new Intent(SERVICE_ACTION);
            serviceIntent.SetPackage(CHROME_PACKAGE);
            var resolveInfos = PackageManager.QueryIntentServices(serviceIntent, 0);
            var a = resolveInfos.ToList();

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnResume()
        {
            base.OnResume();

            Xamarin.Essentials.Platform.OnResume();
        }
    }
}