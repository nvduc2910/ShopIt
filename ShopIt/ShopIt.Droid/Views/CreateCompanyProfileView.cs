using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Webkit;
using Android.Widget;
using Java.IO;
using Java.Util;
using MvvmCross.Platform;
using ShopIt.Core.Services;
using ShopIt.Core.ViewModels;
using ShopIt.Droid.Controls;
using ShopIt.Droid.Extensions;
using ShopIt.Droid.Services;
using ViewModels;
using File = Java.IO.File;
using Uri = Android.Net.Uri;

namespace ShopIt.Droid.Views
{
    [Activity(Theme = "@style/AppTheme", LaunchMode = LaunchMode.SingleTask,
        ScreenOrientation = ScreenOrientation.Portrait, WindowSoftInputMode = SoftInput.AdjustResize)]
    public class CreateCompanyProfileView : BaseView, ICompanyProfileView
    {
        #region UI Controls

        private LinearLayout lnStep1, lnStep2;

        private ProfileStep step1, step2;

        private ScrollView scroll;

        #endregion

        #region Overrides

        public CreateCompanyProfileViewModel ViewModel
        {
            get { return base.ViewModel as CreateCompanyProfileViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.CreateCompanyProfileView);
            ViewModel.View = this;
            Init();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if ((requestCode == ImageService.PICK_IMAGE_FROM_LIB) && (resultCode == Result.Ok) && (data != null))
            {
                var imageService = Mvx.Resolve<IImageService>();
                Stream iStream = ContentResolver.OpenInputStream(data.Data);
                byte[] result = ReadFully(iStream);
                (imageService as ImageService).tcs.SetResult(result);
            }
            if ((requestCode == ImageService.TAKE_IMAGE) && (resultCode == Result.Ok))
            {
                var imageService = Mvx.Resolve<IImageService>();

                var bitmap = this.LoadImageAndResize(DroidConstants.LOCAL_TEMP_IMAGE_FROM_CAMERA, DroidConstants.LOCAL_TEMP_IMAGE_FILE_NAME, DroidConstants.ImageSize, DroidConstants.ImageSize);

                if (bitmap == null)
                {
                    (imageService as ImageService).tcs.SetResult(null);
                }
                else
                {
                    (imageService as ImageService).tcs.SetResult(bitmap.Bytes());
                }

                bitmap = null;

                GC.Collect();
            }
        }
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        #endregion

        #region Implements
        #region ICompanyProfileView
        public void ShowAddGSTRegistered()
        {
        }

        public void HidenAddGSTRegisterd()
        {
        }

        public void ShowAddCompensationCert()
        {
        }

        public void HiddenAddCompensationCert()
        {
        }

        public void ShowAddPublicLiabilityCert()
        {
        }

        public void HidenAddPublicLiabilityCert()
        {
        }

        public void ShowAddProductLiabilityCert()
        {
        }

        public void HidenAddProductLiabilityCert()
        {
        }

        public void ShowAddMoreLicencesCert()
        {
        }

        public void ShowSetTimeDialog()
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                ViewModel.SelectedTimeCommand.Execute(time);
            });
            frag.Show(FragmentManager, "");
        }

        public void HiddenTimeDialog()
        {
        }

        public void ShowAddBusinessAddress()
        {
        }

        public void HidenBusinessAddress()
        {
        }

        public void ScrollToBottom()
        {
            scroll.SmoothScrollTo(0, scroll.Height);
        }

        public void PanToPage(int page)
        {
            switch (page)
            {
                case 1:
                    lnStep1.Visibility = ViewStates.Visible;
                    lnStep2.Visibility = ViewStates.Gone;
                    step1.SetImageResource(Resource.Drawable.ic_progress_bar_filled);
                    step2.SetImageResource(Resource.Drawable.ic_progress_bar_not_filled);
                    break;
                case 2:
                    lnStep1.Visibility = ViewStates.Gone;
                    lnStep2.Visibility = ViewStates.Visible;
                    step1.SetImageResource(Resource.Drawable.ic_progress_bar_filled);
                    step2.SetImageResource(Resource.Drawable.ic_progress_bar_filled);
                    break;
            }
            scroll.SmoothScrollTo(0, 0);
        }

        public void HideKeyboard()
        {
        }
        #endregion

        #endregion

        #region Methods

        void Init()
        {
            lnStep1 = this.FindViewById<LinearLayout>(Resource.Id.lnStep1);
            lnStep2 = this.FindViewById<LinearLayout>(Resource.Id.lnStep2);
            step1 = this.FindViewById<ProfileStep>(Resource.Id.profile_step1);
            step2 = this.FindViewById<ProfileStep>(Resource.Id.profile_step2);
            scroll = this.FindViewById<ScrollView>(Resource.Id.scroll);

            step1.SetImageResource(Resource.Drawable.ic_progress_bar_filled);

            lnStep1.Visibility = ViewStates.Visible;
            lnStep2.Visibility = ViewStates.Gone;
        }

        #endregion

        
    }
}