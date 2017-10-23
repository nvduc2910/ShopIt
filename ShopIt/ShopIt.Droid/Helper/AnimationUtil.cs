using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

namespace ShopIt.Droid.Helper
{
    public class AnimationUtil
    {
        public static void InFromRight(View view,long duration = 250)
        {
            Animation inFromRight = new TranslateAnimation(Dimension.RelativeToParent,1.0f, Dimension.RelativeToParent, 0.0f, Dimension.RelativeToParent, 0.0f, Dimension.RelativeToParent, 0.0f);
            inFromRight.Duration = duration;
            inFromRight.Interpolator = new AccelerateInterpolator();
            inFromRight.AnimationStart += (sender, args) =>
            {
                view.Visibility = ViewStates.Visible;
            };
            view.Animation = inFromRight;
        }

        public static void InFromLeft(View view, long duration = 250)
        {
            Animation inFromRight = new TranslateAnimation(Dimension.RelativeToParent, -1.0f, Dimension.RelativeToParent, 0.0f, Dimension.RelativeToParent, 0.0f, Dimension.RelativeToParent, 0.0f);
            inFromRight.Duration = duration;
            inFromRight.Interpolator = new AccelerateInterpolator();
            inFromRight.AnimationStart += (sender, args) =>
            {
                view.Visibility = ViewStates.Visible;
            };
            view.Animation = inFromRight;
        }

        public static void InFromTop(View view, long duration = 250)
        {
            Animation inFromTop = new TranslateAnimation(Dimension.RelativeToParent, 0.0f, Dimension.RelativeToParent, 0.0f, Dimension.RelativeToParent, -1.0f, Dimension.RelativeToParent, 0.0f);
            inFromTop.Duration = duration;
            inFromTop.Interpolator = new AccelerateInterpolator();
            inFromTop.AnimationStart += (sender, args) =>
            {
                view.Visibility = ViewStates.Visible;
            };
            view.StartAnimation(inFromTop);
        }

        public static void OutToLeft(View view,long duration = 250)
        {
            Animation outToLeft = new TranslateAnimation(Dimension.RelativeToParent, 0.0f, Dimension.RelativeToParent, -1.0f, Dimension.RelativeToParent, 0.0f, Dimension.RelativeToParent, 0.0f);
            outToLeft.Duration = duration;
            outToLeft.Interpolator = new AccelerateInterpolator();
            outToLeft.AnimationStart += (sender, args) =>
            {
                view.Visibility = ViewStates.Visible;
            };
            outToLeft.AnimationEnd += (sender, args) =>
            {
                view.Visibility = ViewStates.Gone;
            };
            view.Animation = outToLeft;
        }
        public static void OutToRight(View view, long duration = 250)
        {
            Animation outToLeft = new TranslateAnimation(Dimension.RelativeToParent, 0.0f, Dimension.RelativeToParent, 1.0f, Dimension.RelativeToParent, 0.0f, Dimension.RelativeToParent, 0.0f);
            outToLeft.Duration = duration;
            outToLeft.Interpolator = new AccelerateInterpolator();
            outToLeft.AnimationStart += (sender, args) =>
            {
                view.Visibility = ViewStates.Visible;
            };
            outToLeft.AnimationEnd += (sender, args) =>
            {
                view.Visibility = ViewStates.Gone;
            };
            view.Animation = outToLeft;
        }
        public static void OutToTop(View view, long duration = 250, Action animationEnded = null)
        {
            Animation outToTop = new TranslateAnimation(Dimension.RelativeToParent, 0.0f, Dimension.RelativeToParent, 0.0f, Dimension.RelativeToParent, 0.0f, Dimension.RelativeToParent, -1.0f);
            outToTop.Duration = duration;
            outToTop.Interpolator = new AccelerateInterpolator();
            outToTop.AnimationStart += (sender, args) =>
            {
                view.Visibility = ViewStates.Visible;
            };
            outToTop.AnimationEnd += (sender, args) =>
            {
                view.Visibility = ViewStates.Gone;
                if (animationEnded != null)
                {
                    animationEnded();
                }
            };
            view.StartAnimation(outToTop);
        }
    }
}