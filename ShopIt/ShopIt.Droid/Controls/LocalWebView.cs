using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Webkit;
using Android.Widget;

namespace ShopIt.Droid.Controls
{
    public class LocalWebView : WebView
    {
        public LocalWebView(Context context) : base(context)
        {
            Init();
        }

        public LocalWebView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init();
        }

        #region Binding Properties

        #region HtmlFile

        public string HtmlFile
        {
            set { LoadHtmlFileContent(value); }
        }

        #endregion

        #endregion

        #region Methods

        void LoadHtmlFileContent(string fileName)
        {
            this.LoadUrl(String.Format("file:///android_asset/{0}",fileName));
        }

        void Init()
        {
            this.Settings.JavaScriptEnabled = true;
        }

        #endregion
    }
}