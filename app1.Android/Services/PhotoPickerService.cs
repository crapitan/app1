using System;
using System.IO;
using System.Threading.Tasks;
using Android.Content;
using app1.Droid.Services;
using app1.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhotoPickerService))]
namespace app1.Droid.Services
{
    public class PhotoPickerService : IPhotoPickerService 
    {
        public Task<Stream> GetImageStreamAsync()
        {
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            MainActivity.Instance.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Picture"),
                MainActivity.PickImageId);

            MainActivity.Instance.PickImageTaskCompletionSource = new TaskCompletionSource<Stream>();

            return MainActivity.Instance.PickImageTaskCompletionSource.Task; 
        }
    }
}
