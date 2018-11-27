using CipherBotApp.Class_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
          
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
         

            try {
               
                string strResult = await api.LoginAsync(entry_user.Text, entry_password.Text);
                DateTime? dateResult = null;
               // await DisplayAlert("Error",strResult, "Ok");
                if (strResult.Length<8)
                {

                    await DisplayAlert("Login Failed", "Username Or Password Incorrect", "Ok");
                    entry_user.Text = null;
                    entry_password.Text = null;
                }
                else
                {
                    dateResult= Convert.ToDateTime(strResult);
                }
                if(dateResult < DateTime.Today)
                {

                    await DisplayAlert("Subscription Expired", "Your Subscription To Cipherbot Has Expired, Please Visit Website To Resubscribe", "Ok");
                    entry_user.Text = null;
                    entry_password.Text = null;
                }
                else
                {
                    GlobalVar.User = entry_user.Text;
                    MainPage main = new MainPage();
                    await Navigation.PushAsync(main);
                }
            }
            catch(Exception eeeee)
            {

                await DisplayAlert("Error", eeeee.ToString(), "Ok");

            }
        }

       
    }
}