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
using MvvmCross.Droid.Platform;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;
using MvvmCross.Platform.Converters;
using MvvmCross.Platform.Platform;
using Services;
using ShopIt.Core;
using ShopIt.Core.Services;
using ShopIt.Droid.Converters;
using ShopIt.Droid.Presenter;
using ShopIt.Droid.Services;

namespace ShopIt.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            Mvx.RegisterSingleton<IDroidService>(new DroidService());
            Mvx.RegisterSingleton<ICacheService>(new CacheService());
            Mvx.RegisterSingleton<IApiService>(new ApiService());
            Mvx.RegisterSingleton<IMessageboxService>(new MessageboxService());
            Mvx.RegisterSingleton<IProgressDialogService>(new ProgressDialogService());
            Mvx.RegisterSingleton<IPlatformService>(new PlatformService());
            Mvx.RegisterSingleton<IImageService>(new ImageService());
            Mvx.RegisterSingleton<ITaskManagementService>(new TaskManagementService());
            Mvx.RegisterSingleton<IAzureService>(new AzureService());
            Mvx.RegisterSingleton<ITrackingService>(new TrackingService());

            return new App();
        }

        protected override void FillValueConverters(IMvxValueConverterRegistry registry)
        {
            base.FillValueConverters(registry);

            registry.AddOrOverwrite("StringToVisibilityConverter", new StringToVisibilityConverter());
            registry.AddOrOverwrite("ReverseBoolConverter", new ReverseBoolConverter());
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var presenter = Mvx.IocConstruct<DroidPresenter>();
            Mvx.RegisterSingleton<IMvxAndroidViewPresenter>(presenter);
            return presenter;
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}