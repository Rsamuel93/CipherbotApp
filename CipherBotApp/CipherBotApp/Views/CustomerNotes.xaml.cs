using CipherBotApp;
using CipherBotApp.Class_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CipherBot.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomerNotes : ContentPage
	{
		public CustomerNotes ()
		{
			InitializeComponent ();
		}
        private ApiServices api = new ApiServices();


        private async void btnCustomerNotes_Clicked(object sender, EventArgs e)
        {


            try
            {

                await api.CustomerNotesAsync(entry_notes.Text, GlobalVar.strGuid, GlobalVar.strfilename, GlobalVar.strMillisecond);
                await DisplayAlert("Notes Added Successfully", "Success", "OK");
                await Navigation.PushAsync(new MainPage());
            }
            catch (Exception)
            {

                await DisplayAlert("Error", "Error", "Ok");

            }
        }
    }
}