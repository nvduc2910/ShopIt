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

namespace ShopIt.Droid.Services
{
    public interface IDroidService
    {
        Context CurrentContext { get; set; }
    }

    public class DroidService : IDroidService
    {
        public Context CurrentContext { get; set; }
    }
}