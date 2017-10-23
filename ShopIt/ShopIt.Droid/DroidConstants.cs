using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ShopIt.Droid
{
    public static class DroidConstants
    {
        public const int ImageSize = 512;
        public const string LOCAL_IMAGE_DIRECTORY = "LocalImageDirectory";
        public const string LOCAL_TEMP_IMAGE_FROM_CAMERA = "LocalTempImageFromCamera";
        public const string LOCAL_TEMP_IMAGE_FILE_NAME = "Temp.png";
    }
}