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
using System.Text.RegularExpressions;
using CipherBot.Views;

namespace CipherBotApp
{
    public partial class MainPage : ContentPage
    {
        int count;
        private string strmillisecond = Convert.ToString(DateTime.Now.Millisecond);
        private MediaFile _Mediafile;
        public MainPage()
        {
            InitializeComponent();
            entry_email.Text = GlobalVar.User;

        }
        Func<object> func = () =>
        {
            var layout = new RelativeLayout();
            var image = new Image
            {
                Source = "Icon.png"
            };

            Func<RelativeLayout, double> ImageHeight = (p) => image.Measure(layout.Width, layout.Height).Request.Height;
            Func<RelativeLayout, double> ImageWidth = (p) => image.Measure(layout.Width, layout.Height).Request.Width;

            layout.Children.Add(image,
                        Constraint.RelativeToParent(parent => parent.Width / 10- ImageWidth(parent) / 50),
                        Constraint.RelativeToParent(parent => parent.Height / 10 - ImageHeight(parent) /5)
            );
            return layout;
        };

        private async void Take_Photo_Button_Clicked(object sender, EventArgs e)
        {
            Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (!reg.IsMatch(entry_email.Text))
            {
                await DisplayAlert("Invalid Email Address", "Please Enter Valid Email Address", "OK");
                return;
            }
           
            if (string.IsNullOrWhiteSpace(entry_email.Text))
            {

                await DisplayAlert("No Email Address", "Please Enter A Email Address", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(entry_email.Text))
                  {

                    await DisplayAlert("No Email Address", "Please Enter A Email Address", "OK");
                     return;
                  }


                if (string.IsNullOrWhiteSpace(entry_filename.Text))
                {
                

                        await DisplayAlert("No File Name", "Please Enter A File Name", "OK");
                        return;
                    }
                  await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)

                {

                    await DisplayAlert("No Camera", ":(No Camera Avaliable", "OK");
                    return;
                }

                try
                {
                    string response = null;

                    while (response != "No")
                    {
                        count++;
                        _Mediafile = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                        {

                            Directory = "Sample",
                            PhotoSize = PhotoSize.MaxWidthHeight,
                            AllowCropping = true,
                            //OverlayViewProvider = func,
                            //MaxWidthHeight = 50,
                            Name = GlobalVar.User+"_" +entry_email.Text  +"_"+ entry_filename.Text + "_" + GlobalVar.strGuid + "_"+ strmillisecond + "_" + count.ToString() + ".jpg"

                        });

                        if (_Mediafile == null)
                            return;

                        var content = new MultipartFormDataContent();

                        content.Add(new StreamContent(_Mediafile.GetStream()),


                        "\"File\"",
                            $"\"{_Mediafile.Path}\"");

                        var httpClient = new HttpClient();

                        var Uploadtoserveraddress = GlobalVar.BaseIPaddress + "upload";

                        var httpresponse = await httpClient.PostAsync(Uploadtoserveraddress, content);


                        response = await DisplayActionSheet(count.ToString() + " Images Uploaded,  Add More?", "Yes", "No");



                    }



                    count = 0;

                  string notes = null;
                  notes = await DisplayActionSheet("Add Notes To File?", "Yes", "No");
                if (notes != "Yes")
                {

                    await Navigation.PushAsync(new MainPage());
                }
                else
                {
                    GlobalVar.strfilename = entry_filename.Text;
                    GlobalVar.strMillisecond = strmillisecond;
                    await Navigation.PushAsync(new CustomerNotes());
                }
                }
                catch (Exception ee)

                {
                    await DisplayAlert("No Camera","Error", "OK");
                }


                if (_Mediafile == null)
                    return;

            
         




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

                //remotepathlabel.Text = await httpresponse.Content.ReadAsStringAsync();
                await DisplayAlert("", "Photo Uploaded", "OK");
            }
            catch (Exception eeee)
            {
                await DisplayAlert("", eeee.ToString(), "OK");

            }
        }

        private async void Pick_Photo_Button_Clicked(object sender, EventArgs e)

        {

            if (!CrossMedia.Current.IsPickPhotoSupported)

            {

                await DisplayAlert("Photos Not Supported", "Permission not granted to photos.", "OK");

                return;

            }

            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions

            {

                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,



            });





            if (file == null)

                return;



            image.Source = ImageSource.FromStream(() =>

            {

                var stream = file.GetStream();

                file.Dispose();

                return stream;

            });

        }


    }
    
}
