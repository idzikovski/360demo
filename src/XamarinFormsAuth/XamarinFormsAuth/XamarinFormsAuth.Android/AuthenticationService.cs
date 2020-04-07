using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.CustomTabs;
using Android.Support.CustomTabs.Chromium.SharedUtilities;
using Android.Views;
using Android.Widget;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using XamarinFormsAuth.Droid;

[assembly: Dependency(typeof(AuthenticationService))]
namespace XamarinFormsAuth.Droid
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly string CHROME_PACKAGE = "com.android.chrome";
        private readonly string OPERA_PACKAGE = "com.opera.mini.native";

        public static TaskCompletionSource<string> TaskCompletionSource;

        CustomTabsServiceConnection connection;
        CustomTabsClient client;


        public Task<string> LoginAsync()
        {
            TaskCompletionSource = new TaskCompletionSource<string>();

            string url = "https://oauthdemo.000webhostapp.com/test.html?" +
                "scope=email%20profile&" +
                "response_type=code&" +
                "redirect_uri=com.companyname.xamarinformsauth%3A/oauth2redirect&" +
                "client_id=1056433391892-gboradv67bghftbf41iee3q5ueflp6gb.apps.googleusercontent.com";


            Intent activityIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse("https://oauthdemo.000webhostapp.com/test.html"));
            PackageManager pm = CrossCurrentActivity.Current.Activity.PackageManager;
            List<ResolveInfo> resolvedActivityList = pm.QueryIntentActivities(
                    activityIntent, PackageInfoFlags.MatchAll).ToList();
            foreach (var info in resolvedActivityList)
            {
                Intent serviceIntent = new Intent();
                serviceIntent.SetAction("android.support.customtabs.action.CustomTabsService");
                serviceIntent.SetPackage(info.ActivityInfo.PackageName);
                if (pm.ResolveService(serviceIntent, 0) != null)
                {
                    System.Diagnostics.Debug.WriteLine($"{info.ActivityInfo.PackageName} supports custom tabs");
                }
            }




            var ok = CustomTabsClient.BindCustomTabsService(Android.App.Application.Context, "org.mozilla.firefox", new MyServiceConnection((c) =>
            {
                client = c;

                var session = client.NewSession(new CallBackHandler((e, b) =>
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }));



                CustomTabsIntent.Builder builder = new CustomTabsIntent.Builder(session);
                CustomTabsIntent customTabsIntent = builder.Build();
                TrustedWebUtils.LaunchAsTrustedWebActivity(CrossCurrentActivity.Current.Activity, customTabsIntent, Android.Net.Uri.Parse(url));
                //customTabsIntent.LaunchUrl(Android.App.Application.Context, Android.Net.Uri.Parse(url));



            }));

            CustomTabsIntent.Builder builder = new CustomTabsIntent.Builder();
            CustomTabsIntent customTabsIntent = builder.Build();
            customTabsIntent.LaunchUrl(CrossCurrentActivity.Current.Activity, Android.Net.Uri.Parse(url));


            return TaskCompletionSource.Task;
        }

        private void OnConnected(CustomTabsClient client)
        {
            this.client = client;
        }

        public class CallBackHandler : CustomTabsCallback
        {
            private readonly Action<int, Bundle> navigationEventAction;

            public CallBackHandler(Action<int, Bundle> navigationEvent)
            {
                this.navigationEventAction = navigationEvent;
            }

            public override void OnNavigationEvent(int navigationEvent, Bundle extras)
            {
                base.OnNavigationEvent(navigationEvent, extras);
                navigationEventAction?.Invoke(navigationEvent, extras);
            }
        }
    }
}