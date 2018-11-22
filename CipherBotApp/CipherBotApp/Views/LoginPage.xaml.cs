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
        private void btnLogin_Clicked(object sender, EventArgs e)
        {
          var test =  api.LoginAsync(entry_user.Text, entry_password.Text);
        }
    }
}