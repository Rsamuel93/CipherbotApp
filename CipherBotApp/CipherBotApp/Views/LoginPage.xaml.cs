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
        public LoginPage ()
		{
			InitializeComponent ();
		}
        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            try {
                await DisplayAlert("Login Failed", "Username Or Password Incorrect", "Ok");
                string strResult = await api.LoginAsync(entry_user.Text, entry_password.Text);
                DateTime? dateResult = null;

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

                    await DisplayAlert("Subscription Expired", "Your Subscription To Cipherbot Has Expired, Please Visit Website To Resubscrive", "Ok");
                    entry_user.Text = null;
                    entry_password.Text = null;
                }
                else
                {
                    MainPage main = new MainPage();
                    await App.Current.MainPage.Navigation.PushAsync(main);
                }
            }
            catch(Exception eeeee)
            {

                await DisplayAlert("Error", eeeee.ToString(), "Ok");

            }
        }

        private void btn_test_clicked(object sender, EventArgs e)
        {
            DisplayAlert("", "", "");
        }
    }
}