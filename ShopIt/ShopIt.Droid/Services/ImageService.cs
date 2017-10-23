using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using MvvmCross.Platform;
using ShopIt.Core;
using ShopIt.Core.Services;
using ShopIt.Droid.Extensions;
using Uri = Android.Net.Uri;

namespace ShopIt.Droid.Services
{
    public class ImageService : IImageService
    {
        public static int PICK_IMAGE_FROM_LIB = 1000;
        public static int TAKE_IMAGE = 2000;

        public TaskCompletionSource<byte[]> tcs { get; set; }

        public Task<byte[]> TakePicture()
        {
            if (tcs != null)
            {
                tcs.TrySetResult(null);
                tcs = null;
            }

            tcs = new TaskCompletionSource<byte[]>();

            Activity currentActivity = Mvx.Resolve<IDroidService>().CurrentContext as Activity;

            Intent intent = new Intent();
            intent.SetAction(MediaStore.ActionImageCapture);
            File tempImageFile = currentActivity.GetOutputMediaFileName(DroidConstants.LOCAL_TEMP_IMAGE_FROM_CAMERA,DroidConstants.LOCAL_TEMP_IMAGE_FILE_NAME);
            Uri photoUri = Uri.FromFile(tempImageFile);
            intent.PutExtra(MediaStore.ExtraOutput, photoUri);
            currentActivity.StartActivityForResult(intent, TAKE_IMAGE);

            return tcs.Task;
        }

        public Task<byte[]> SelectFromLibrary()
        {
            if (tcs != null)
            {
                tcs.TrySetResult(null);
                tcs = null;
            }

            tcs = new TaskCompletionSource<byte[]>();

            Activity currentActivity = Mvx.Resolve<IDroidService>().CurrentContext as Activity;

            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);
            currentActivity.StartActivityForResult(Intent.CreateChooser(intent, "Select Picture"), PICK_IMAGE_FROM_LIB);

            return tcs.Task;
        }

        public Task<string> SaveImage(string imageName, byte[] data)
        {
            Context ct = Mvx.Resolve<IDroidService>().CurrentContext;
            Bitmap bitmap = BitmapFactory.DecodeByteArray(data, 0, data.Length);
            return Task.FromResult(bitmap.StoreImage(ct, DroidConstants.LOCAL_IMAGE_DIRECTORY, imageName));
        }

        public Task<bool> DeleteImage(string imageName)
        {
            return Task.FromResult(false);
        }

        public byte[] GetByteFromImage(string path)
        {
            return null;
        }
    }
}