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
using MvvmCross.Platform;
using ShopIt.Core;
using ShopIt.Droid.Extensions;
using ShopIt.Droid.Services;
using UniversalImageLoader.Core;
using UniversalImageLoader.Core.Assist;
using UniversalImageLoader.Core.Listener;

namespace ShopIt.Droid.Controls
{
    public class BindableImageView : ImageView
    {
        public BindableImageView(Context context) : base(context)
        {
        }

        public BindableImageView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public BindableImageView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        #region Binding Properties

        private string mImageFileName;

        public string ImageFileName
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
                    imageLoader.LoadImage(value,option,new ImageLoadingListener(loadingComplete: (imageUri, view, loadedImage) =>
                    {
                        this.SetImageBitmap(loadedImage);
                    }));
                }
            }
        }

        private string mImageLocal;

        public string ImageLocal
        {
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    if (mImageLocal == value)
                        return;
                    var imageLoader = ImageLoader.Instance;
                    var ct = Mvx.Resolve<IDroidService>().CurrentContext;
                    var bitmap = ct.LoadImage(DroidConstants.LOCAL_IMAGE_DIRECTORY, value);
                    this.SetImageBitmap(bitmap);
                }
            }
        }

        #endregion
    }
}