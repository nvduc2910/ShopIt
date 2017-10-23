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
    public class EditProfileView : BaseView , IEditProfileView
    {
        #region UI Controls

        private LinearLayout newInsure;
        private LinearLayout existedInsure;
        private LinearLayout newFirstAid;
        private LinearLayout existFirstAid;
        private LinearLayout newLiability;
        private LinearLayout existLiability;
        private LinearLayout newCompanyPublicLiability;
        private LinearLayout existCompanyPublicLiability;
        private LinearLayout newCompanyProductLiability;
        private LinearLayout existCompanyProductLiability;
        private LinearLayout newCompanyCompensation;
        private LinearLayout existCompanyCompensation;

        #endregion

        #region Overrides

        public EditProfileViewModel ViewModel
        {
            get { return base.ViewModel as EditProfileViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.EditProfileView);
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
        #region IEditProfileView
        public void HidenKeyboard()
        {
        }

        public void ShowTradeContractorCert()
        {
        }

        public void HidenTradeContractorCert()
        {
        }

        public void ShowOperationCert()
        {
        }

        public void HidenOperationCert()
        {
        }

        public void ShowCompanyProfile()
        {
        }

        public void HideCompanyProfile()
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

        public void ShowLicences()
        {
        }

        public void HidenLicences()
        {
        }

        public void ShowWorksCompenstation()
        {
            newCompanyCompensation.Visibility = ViewStates.Gone;
            existCompanyCompensation.Visibility = ViewStates.Visible;
        }

        public void HidenWordsCompenstation()
        {
        }

        public void ShowPublicLiability()
        {
            newCompanyPublicLiability.Visibility = ViewStates.Gone;
            existCompanyPublicLiability.Visibility =ViewStates.Visible;
        }

        public void HidenPublicLiability()
        {
        }

        public void ShowProductLiability()
        {
            newCompanyProductLiability.Visibility = ViewStates.Gone;
            existCompanyProductLiability.Visibility = ViewStates.Visible;
        }

        public void HidenProductLiability()
        {
        }

        public void ShowAddress()
        {
        }

        public void HidenAddress()
        {
        }

        public void AddMoreOperationalCert()
        {
        }

        public void RemoveOperationalCert()
        {
        }

        public void AddMoreLicences()
        {
        }

        public void RemoveLicences()
        {
        }

        public void ShowAddWordCompenstation()
        {
            newCompanyCompensation.Visibility = ViewStates.Visible;
            existCompanyCompensation.Visibility = ViewStates.Gone;
        }

        public void HiddenAddWordCompenstation()
        {
        }

        public void ShowAddPublicLiability()
        {
            newCompanyPublicLiability.Visibility = ViewStates.Visible;
            existCompanyPublicLiability.Visibility = ViewStates.Gone;
        }

        public void HiddenAddPublicLiabiliy()
        {
        }

        public void ShowAddProductLiability()
        {
            newCompanyProductLiability.Visibility = ViewStates.Visible;
            existCompanyProductLiability.Visibility = ViewStates.Gone;
        }

        public void HiddenAddProductLiability()
        {
        }

        public void ShowIncomeInsured()
        {
            existedInsure.Visibility = ViewStates.Visible;
            newInsure.Visibility = ViewStates.Gone;
        }

        public void HidenIncomeInsured()
        {
        }

        public void ShowFirstAid()
        {
            existFirstAid.Visibility = ViewStates.Visible;
            newFirstAid.Visibility = ViewStates.Gone;
        }

        public void HidenFirstAid()
        {
        }

        public void ShowPerPublicLiablity()
        {
            newLiability.Visibility = ViewStates.Gone;
            existLiability.Visibility = ViewStates.Visible;
        }

        public void HidenPerPublicLiability()
        {
        }

        public void ShowAddIncomeInsured()
        {
            newInsure.Visibility = ViewStates.Visible;
            existedInsure.Visibility = ViewStates.Gone;
        }

        public void HidenAddIncomeInsured()
        {
        }

        public void ShowAddFirstAid()
        {
            newFirstAid.Visibility = ViewStates.Visible;
            existFirstAid.Visibility = ViewStates.Gone;
        }

        public void HidenAddFirstAid()
        {
        }

        public void ShowAddPerPublicLiability()
        {
            newLiability.Visibility = ViewStates.Visible;
            existLiability.Visibility = ViewStates.Gone;
        }

        public void HidenAddPerPublicLiability()
        {
        }

        #endregion

        #endregion

        #region Methods

        void Init()
        {
            newInsure = this.FindViewById<LinearLayout>(Resource.Id.newInsure);
            existedInsure = this.FindViewById<LinearLayout>(Resource.Id.existedInsure);
            newFirstAid = this.FindViewById<LinearLayout>(Resource.Id.newFirstAid);
            existFirstAid = this.FindViewById<LinearLayout>(Resource.Id.existedFirstAid);
            newLiability = this.FindViewById<LinearLayout>(Resource.Id.newLiability);
            existLiability = this.FindViewById<LinearLayout>(Resource.Id.existedLiability);
            newCompanyPublicLiability = this.FindViewById<LinearLayout>(Resource.Id.newCompanyPublicLiability);
            existCompanyPublicLiability = this.FindViewById<LinearLayout>(Resource.Id.existCompanyPublicLiability);
            newCompanyProductLiability = this.FindViewById<LinearLayout>(Resource.Id.newCompanyProductLiability);
            existCompanyProductLiability = this.FindViewById<LinearLayout>(Resource.Id.existCompanyProductLiability);
            newCompanyCompensation = this.FindViewById<LinearLayout>(Resource.Id.newCompanyCompensation);
            existCompanyCompensation = this.FindViewById<LinearLayout>(Resource.Id.existCompanyCompensation);
        }

        #endregion
    }
}