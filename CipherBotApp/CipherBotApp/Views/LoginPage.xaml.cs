using Android.App;
using Android.Content;
using Android.Provider;
using CipherBot.Class_s;
using CipherBotApp.Class_s;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CipherBotApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
    {
     
      
        private ApiServices api = new ApiServices();
        public LoginPage()
        {
            
         
            InitializeComponent();
            //DependencyService.Get<IMediaService>().ClearFiles(_images);

        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            

                try
                {
               
                string strResult = await api.LoginAsync(entry_user.Text, entry_password.Text);
              //  await DisplayAlert("Login Failed", strResult, "Ok");
                strResult = strResult.Replace("/", "-");
                strResult = strResult.Replace(" ", null);
                strResult = strResult.Trim('"');
       

                DateTime dateResult;
                if (!DateTime.TryParseExact(strResult, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dateResult))
                {
                   
                
                   await DisplayAlert("Login Failed", "Username Or Password Incorrect", "Ok");
                    entry_user.Text = null;
                    entry_password.Text = null;
                    return;

                }
               
                if(dateResult < DateTime.Today)
                {

                    await DisplayAlert("Subscription Expired", "Your Subscription Has Expired, Please Visit Website To Resubscribe", "Ok");
                    entry_user.Text = null;
                    entry_password.Text = null;
                    return;
                }
                else
                {
                    GlobalVar.User = entry_user.Text;
                    await Navigation.PushAsync(new MainPage());
                }
            }
            catch(Exception)
            {

                await DisplayAlert("Error","Error", "Ok");

            }
        }
      

       

    
    }
}