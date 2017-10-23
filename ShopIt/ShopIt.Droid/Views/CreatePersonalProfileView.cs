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
using Android.Media;
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
using Stream = System.IO.Stream;
using Uri = Android.Net.Uri;

namespace ShopIt.Droid.Views
{
    [Activity(Theme = "@style/AppTheme", LaunchMode = LaunchMode.SingleTask,
        ScreenOrientation = ScreenOrientation.Portrait,WindowSoftInputMode = SoftInput.AdjustResize)]
    public class CreatePersonalProfileView : BaseView, IPersonalProfileView
    {
        #region UI Controls

        private LinearLayout lnStep1, lnStep2, lnStep3, lnStep4, complete, stephightlight;

        private RelativeLayout step;

        private EditText etEmailInvite;

        private ScrollView scroll;

        public static readonly int PickImageId = 1000;


        #endregion

        #region Overrides

        public CreatePersonalProfileViewModel ViewModel
        {
            get { return base.ViewModel as CreatePersonalProfileViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.CreatePersonalProfileView);
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

        #region IPersonalProfileView


        public void ShowPopupFinish()
        {
            step.Visibility= ViewStates.Gone;
            stephightlight.Visibility = ViewStates.Gone;
            scroll.Visibility = ViewStates.Gone;
            complete.Visibility = ViewStates.Visible;
        }

        public void HiddenPopupFinish()
        {
        }

        public void PanToPage(int page)
        {
            switch (page)
            {
                case 1:
                    lnStep1.Visibility = ViewStates.Visible;
                    lnStep2.Visibility = ViewStates.Gone;
                    lnStep3.Visibility = ViewStates.Gone;
                    lnStep4.Visibility = ViewStates.Gone;
                    complete.Visibility = ViewStates.Gone;
                    break;
                case 2:
                    lnStep1.Visibility = ViewStates.Gone;
                    lnStep2.Visibility = ViewStates.Visible;
                    lnStep3.Visibility = ViewStates.Gone;
                    lnStep4.Visibility = ViewStates.Gone;
                    complete.Visibility = ViewStates.Gone;
                    break;
                case 3:
                    lnStep1.Visibility = ViewStates.Gone;
                    lnStep2.Visibility = ViewStates.Gone;
                    lnStep3.Visibility = ViewStates.Visible;
                    lnStep4.Visibility = ViewStates.Gone;
                    complete.Visibility = ViewStates.Gone;
                    break;
                case 4:
                    lnStep1.Visibility = ViewStates.Gone;
                    lnStep2.Visibility = ViewStates.Gone;
                    lnStep3.Visibility = ViewStates.Gone;
                    lnStep4.Visibility = ViewStates.Visible;
                    complete.Visibility = ViewStates.Gone;
                    break;

            }
            scroll.SmoothScrollTo(0, 0);
        }

        public void AddMoreOperationalCert()
        {

        }

        public void ShowSetTimeDialog()
        {
            //TO DO Show diaglog 

            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                ViewModel.SelectedTimeCommand.Execute(time);
            });
            frag.Show(FragmentManager,"");
        }

        public void HiddenTimeDialog()
        {
        }

        public void HideKeyboard()
        {
        }

        public void ScrollToBottom()
        {
            scroll.SmoothScrollTo(0, scroll.Height);
        }

        public byte[] GetByteImage(string path)
        {
            BitmapFactory.Options options = new BitmapFactory.Options();
            options.InPreferredConfig = Bitmap.Config.Rgb565;
            var bitmap = this.LoadImage(DroidConstants.LOCAL_IMAGE_DIRECTORY, path, options);
            return bitmap.Bytes();
        }

        public void ShowIncomeInsured()
        {
            
        }

        public void HidenIncomeInsured()
        {
           
        }

        public void ShowFirstAid()
        {
            
        }

        public void HidenFirstAid()
        {
            
        }

        public void ShowPublicLiability()
        {
            
        }

        public void HidenPublicLiability()
        {
           
        }

        #endregion

        #endregion

        #region Methods

        void Init()
        {
            lnStep1 = this.FindViewById<LinearLayout>(Resource.Id.lnStep1);
            lnStep2 = this.FindViewById<LinearLayout>(Resource.Id.lnStep2);
            lnStep3 = this.FindViewById<LinearLayout>(Resource.Id.lnStep3);
            lnStep4 = this.FindViewById<LinearLayout>(Resource.Id.lnStep4);
            complete = this.FindViewById<LinearLayout>(Resource.Id.complete);
            step = this.FindViewById<RelativeLayout>(Resource.Id.step);
            stephightlight = this.FindViewById<LinearLayout>(Resource.Id.stephightlight);
            scroll = this.FindViewById<ScrollView>(Resource.Id.scroll);

            lnStep1.Visibility = ViewStates.Visible;
            lnStep2.Visibility = ViewStates.Gone;
            lnStep3.Visibility = ViewStates.Gone;
            lnStep4.Visibility = ViewStates.Gone;

            //step.Visibility = ViewStates.Gone;
            //stephightlight.Visibility = ViewStates.Gone;
            //scroll.Visibility = ViewStates.Gone;
            //complete.Visibility = ViewStates.Visible;
        }

        #endregion

        
    }
}