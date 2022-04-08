using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exercises.Models;
using Xamarin.Forms;
using Exercises.Droid;

[assembly: Dependency(typeof(ImagePicker))]
namespace Exercises.Droid
{
    public class ImagePicker : IImagePicker
    {
        public Task<Stream> GetImageStream()
        {
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);
            MainActivity.Instance.taskCompletion = new TaskCompletionSource<Stream>();
            MainActivity.Instance.StartActivityForResult(Intent.CreateChooser(intent, "Vyberte obrázek"), MainActivity.ImgPicker);
            

            return MainActivity.Instance.taskCompletion.Task;
        }
    }
}