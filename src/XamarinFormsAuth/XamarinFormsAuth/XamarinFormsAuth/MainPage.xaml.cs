using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace XamarinFormsAuth
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        IAuthenticationService authenticationService;
        public MainPage()
        {
            InitializeComponent();
            authenticationService = DependencyService.Get<IAuthenticationService>();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var res = await authenticationService.LoginAsync();
        }
    }
}
