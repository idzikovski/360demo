using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFormsAuth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            webview.Navigating += Webview_Navigating;

            webview.Source = "https://www.facebook.com/v6.0/dialog/oauth?" +
                "scope=email&" +
                "response_type=code&" +
                "redirect_uri=https://www.facebook.com/connect/login_success.html&" +
                "client_id=838339906667838";

        }

        private void Webview_Navigating(object sender, WebNavigatingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Url);
        }
    }
}