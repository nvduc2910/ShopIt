using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using ImageViews.Photo;
using UniversalImageLoader.Core;
using UniversalImageLoader.Core.Assist;
using UniversalImageLoader.Core.Listener;

namespace ShopIt.Droid.Controls
{
    public class BindablePhotoView : PhotoView
    {
        #region Constructors

        public BindablePhotoView(Context p0, IAttributeSet p1, int p2) : base(p0, p1, p2)
        {
        }

        public BindablePhotoView(Context p0, IAttributeSet p1) : base(p0, p1)
        {
        }

        public BindablePhotoView(Context p0) : base(p0)
        {
        }

        #endregion

        #region Bindings Properties

        #region ImageUrl

        private string mImageFileName;

        public string ImageUrl
        {
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    if (mImageFileName == value)
                        return;
                    var imageLoader = ImageLoader.Instance;
                    var option = new DisplayImageOptions.Builder().ResetViewBeforeLoading(true)
                        .BitmapConfig(Bitmap.Config.Rgb565)
                        .ImageScaleType(ImageScaleType.Exactly)
                        .CacheInMemory(true)
                        .CacheOnDisk(true)
                        .Build();
                    imageLoader.LoadImage(value, option, new ImageLoadingListener(loadingComplete: (imageUri, view, loadedImage) =>
                    {
                        this.SetImageBitmap(loadedImage);
                    }));
                }
            }
        }

        #endregion

        #endregion
    }
}