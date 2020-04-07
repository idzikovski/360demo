using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamarinFormsAuth.Droid
{
    [Activity(Label = "ActivityCustomUrlSchemeInterceptor")]
    [IntentFilter(
        new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataScheme = "com.companyname.xamarinformsauth",
        DataPath = "oauth2redirect")]
    public class ActivityCustomUrlSchemeInterceptor : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            global::Android.Net.Uri uri_android = Intent.Data;

#if DEBUG
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("ActivityCustomUrlSchemeInterceptor.OnCreate()");
            sb.Append("     uri_android = ").AppendLine(uri_android.ToString());
            System.Diagnostics.Debug.WriteLine(sb.ToString());
#endif

            // Convert iOS NSUrl to C#/netxf/BCL System.Uri - common API
            Uri uri_netfx = new Uri(uri_android.ToString());

            if (AuthenticationService.TaskCompletionSource != null)
            {
                AuthenticationService.TaskCompletionSource.SetResult(uri_android.ToString());
            }

            // load redirect_url Page
            //WebAuthenticator wa = 
            //    (WebAuthenticator)AuthenticatorPageRenderer.Authenticator;

            //wa?.OnPageLoading(uri_netfx);


            this.Finish();

            return;
        }
    }
}
