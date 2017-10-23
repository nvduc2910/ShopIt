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
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;
using ShopIt.Core.Helpers;

namespace ShopIt.Droid.Presenter
{
    public class DroidPresenter : MvxAndroidViewPresenter
    {
        private readonly IMvxViewModelLoader _viewModelLoader;

        public DroidPresenter(IMvxViewModelLoader viewModelLoader)
        {
            _viewModelLoader = viewModelLoader;
        }

        public override void Show(MvxViewModelRequest request)
        {
            if (request.PresentationValues != null)
            {
                if (request.PresentationValues.ContainsKey(PresentationBundleFlagKeys.ClearStack))
                {
                    var requestTranslator = Mvx.Resolve<IMvxAndroidViewModelRequestTranslator>();
                    var intent = requestTranslator.GetIntentFor(request);
                    intent.SetFlags(ActivityFlags.ClearTask);
                    Activity.StartActivity(intent);
                    return;
                }
                else if (request.PresentationValues.ContainsKey(PresentationBundleFlagKeys.ShowBack))
                {
                    var requestTranslator = Mvx.Resolve<IMvxAndroidViewModelRequestTranslator>();
                    var intent = requestTranslator.GetIntentFor(request);
                    intent.SetFlags(ActivityFlags.ClearTop);
                    Activity.StartActivity(intent);
                    return;
                }else if (request.PresentationValues.ContainsKey(PresentationBundleFlagKeys.CloseCurrentAndShow))
                {
                    var requestTranslator = Mvx.Resolve<IMvxAndroidViewModelRequestTranslator>();
                    var intent = requestTranslator.GetIntentFor(request);
                    intent.SetFlags(ActivityFlags.ClearTop);
                    Activity.StartActivity(intent);
                    Activity.Finish();
                    return;
                }
            }

            base.Show(request);
        }
    }
}