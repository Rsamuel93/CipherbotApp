using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CipherBotApp.Class_s;
using Xamarin.Forms;


namespace CipherBotApp
{
    public partial class MainPage : ContentPage
    {
        private MediaFile _Mediafile;
        public MainPage()
        {
            InitializeComponent();
        }
        private async void Take_Photo_Button_Clicked(object sender, EventArgs e)
        {


            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)

            {

                await DisplayAlert("No Camera", ":(No Camera Avaliable", "OK");
                return;
            }

            try
            {


                _Mediafile = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                {

                    Directory = "Sample",
                    PhotoSize = PhotoSize.Medium,
                    CompressionQuality = 90,
                    Name = $"{DateTime.UtcNow}.jpg"

                });
            }
            catch (Exception ee)

            {
            }

            if (_Mediafile == null)
                return;

            DirectoryLabel.Text = _Mediafile.Path;
            image.Source = ImageSource.FromStream(() =>
            {

                return _Mediafile.GetStream();
            });



        }
        private async void Upload_Photo_Button_Clicked(object sender, EventArgs e)
        {
            try
            {


                var content = new MultipartFormDataContent();

                content.Add(new StreamContent(_Mediafile.GetStream()),


                "\"File\"",
                    $"\"{_Mediafile.Path}\"");

                var httpClient = new HttpClient();

                var Uploadtoserveraddress = GlobalVar.BaseIPaddress + "upload";

                var httpresponse = await httpClient.PostAsync(Uploadtoserveraddress, content);

                remotepathlabel.Text = await httpresponse.Content.ReadAsStringAsync();
                await DisplayAlert("", "Photo Uploaded", "OK");
            }
            catch (Exception eee)
            {
                await DisplayAlert("", eee.ToString(), "OK");

            }


        }
    }
}
