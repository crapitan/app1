using System;
using System.Collections.ObjectModel;
using app1.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace app1
{
    public partial class CameraPage : ContentPage
    {

        Image image = new Image(); 
        ObservableCollection<MediaModel> Photos = new ObservableCollection<MediaModel>(); 

        public CameraPage()
        {
            InitializeComponent();
           
           
        }

        private MediaFile _mediaFile;
        private string URL { get; set; }

       private async void OnPickPhotoButtonClicked(object sender, EventArgs e)
        {
            //  (sender as Button).IsEnabled = false;

            //  Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            //  if (stream != null)
            //   {

            //       image.Source = ImageSource.FromStream(() => stream);
            //  }

            // (sender as Button).IsEnabled = true;

            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Error", "This is not support on your device.", "OK");
                return;
            }
            else
            {
                var mediaOption = new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium
                };
                _mediaFile = await CrossMedia.Current.PickPhotoAsync();
                imageView.Source = ImageSource.FromStream(() => _mediaFile.GetStream());
                UploadedUrl.Text = "Image URL:";
            }
        }

        

        private async void btnUpload_Clicked(object sender, EventArgs e)
        {
            if (_mediaFile == null)
            {
                await DisplayAlert("Error", "There was an error when trying to get your image.", "OK");
                return;
            }
            else {
          //      UploadImage(_mediaFile.GetStream()); 
            }
        }





        private void galleryButton_Pressed(System.Object sender, System.EventArgs e)
        {

        }


        private async void photoButton_Pressed(System.Object sender, System.EventArgs e)
        {
            var isInitialize = await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported || !CrossMedia.IsSupported || !isInitialize)
            {
                await DisplayAlert("Error", "Kameran är inte tillgänglig", "Ok");
                return; 
            }

            var newPhotoID = Guid.NewGuid();
            
            using (var photo = await CrossMedia.Current.TakePhotoAsync(new StoreVideoOptions()
            {
                Name = newPhotoID.ToString(),
                SaveToAlbum = true,
                DefaultCamera = CameraDevice.Rear,
                Directory = "Demo Camera",
                CustomPhotoSize = 50


                }))
            {
                if (string.IsNullOrWhiteSpace(photo?.Path))
                {
                    return; 
                }

                var newPhotoMedia = new MediaModel()
                {
                    MediaID = newPhotoID,
                    Path = photo.Path,
                    LocalDateTime = DateTime.Now

                };

                Photos.Add(newPhotoMedia);

                photo.Dispose(); 
            }
        }
    }
}
