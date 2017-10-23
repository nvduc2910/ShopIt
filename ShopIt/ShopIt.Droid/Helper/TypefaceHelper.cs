using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ShopIt.Droid.Helper
{
    public static class TypefaceHelper
    {
        public const string TEXT_BOLD = "Bold";
        public const string TEXT_REGULAR = "Regular";
        public const string TEXT_LIGHT = "Light";

        private static Typeface mBoldTypeFace;
        private static Typeface mRegularTypeFace;
        private static Typeface mLightTypeFace;

        public static Typeface GeTypeface(Context context, string type = "")
        {
            switch (type)
            {
                case TEXT_BOLD:
                    return mBoldTypeFace ??
                           (mBoldTypeFace = Typeface.CreateFromAsset(context.Assets, "Fonts/OpenSans-Bold.ttf"));

                case TEXT_REGULAR:
                    return mRegularTypeFace ??
                           (mRegularTypeFace = Typeface.CreateFromAsset(context.Assets, "Fonts/OpenSans-Regular.ttf"));

                case TEXT_LIGHT:
                    return mLightTypeFace ??
                           (mLightTypeFace = Typeface.CreateFromAsset(context.Assets, "Fonts/OpenSans-Light.ttf"));
            }

            return mRegularTypeFace ??
                   (mRegularTypeFace = Typeface.CreateFromAsset(context.Assets, "Fonts/OpenSans-Regular.ttf"));
        }
    }
}