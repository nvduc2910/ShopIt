using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Binding.Droid.Views;

namespace ShopIt.Droid.Controls
{
    public interface ICustomItemClick
    {
        void ItemClick(int pos);
    }

    public class RecurringAdapter : MvxAdapter
    {
        private float currentTranslationX;
        private float currentX;
        private int mFrontWidth;

        public RecurringAdapter(Context context) : base(context)
        {
            mFrontWidth = context.Resources.GetDimensionPixelSize(Resource.Dimension.marginBasex30);
        }

        public RecurringAdapter(Context context, IMvxAndroidBindingContext bindingContext)
            : base(context, bindingContext)
        {
            mFrontWidth = context.Resources.GetDimensionPixelSize(Resource.Dimension.marginBasex30);
        }

        public ICustomItemClick CustomItemClick { get; set; }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = base.GetView(position, convertView, parent);
            var back = view.FindViewById<LinearLayout>(Resource.Id.back);
            //var front = view.FindViewById<LinearLayout>(Resource.Id.front);

            back.Click -= ItemOnClick;
            back.Click += ItemOnClick;
            back.Tag = view;
           

            view.Touch -= ItemOnTouch;
            view.Touch += ItemOnTouch;
            view.Tag = position;
            return view;
        }


        private void ItemOnClick(object sender, EventArgs eventArgs)
        {
            var view = (View)sender;

            if (view != null)
            {
                var itemView = (View)view.Tag;
                var back = itemView.FindViewById<LinearLayout>(Resource.Id.back);
                var front = itemView.FindViewById<LinearLayout>(Resource.Id.front);
                CloseMenu(back, front, back.TranslationX);
            }
        }

        private void ItemOnTouch(object sender, View.TouchEventArgs touchEventArgs)
        {
            var view = (View)sender;

            if (view == null)
                return;

            var pos = (int)view.Tag;


            var x = touchEventArgs.Event.GetX();
            var back = view.FindViewById<LinearLayout>(Resource.Id.back);
            var front = view.FindViewById<LinearLayout>(Resource.Id.front);

            switch (touchEventArgs.Event.Action)
            {
                case MotionEventActions.Down:
                    currentX = x;
                    currentTranslationX = back.TranslationX;
                    break;

                case MotionEventActions.Move:
                    float distance = Math.Min(mFrontWidth,Math.Abs(x - currentX));
                    if (x - currentX > 0)
                    {
                        back.TranslationX = currentTranslationX  + distance;
                        front.TranslationX = back.TranslationX - mFrontWidth;
                    }
                    else
                    {
                        back.TranslationX = currentTranslationX  - distance;
                        front.TranslationX = back.TranslationX - mFrontWidth;
                    }
                    break;
                case MotionEventActions.Cancel:
                case MotionEventActions.Up:
                    if (x - currentX > 30)
                    {
                        CloseMenu(back, front, back.TranslationX);
                    }
                    else if (x - currentX < -30)
                    {
                        OpenMenu(back, front, back.TranslationX);
                    }
                    else
                    {
                        if (Math.Abs(x - currentX) < 5
                            && CustomItemClick != null
                            && touchEventArgs.Event.Action == MotionEventActions.Up)
                        {
                            CustomItemClick.ItemClick(pos);
                            CloseMenu(back, front, back.TranslationX);
                            break;
                        }

                        if (back.TranslationX > mFrontWidth / 2f)
                        {
                            CloseMenu(back, front, back.TranslationX);
                        }
                        else if (back.TranslationX <= mFrontWidth / 2f)
                        {
                            OpenMenu(back, front, back.TranslationX);
                        }
                    }

                    break;
            }
        }

        private void OpenMenu(View back, View front, float x)
        {
            var anim = ValueAnimator.OfFloat(x, 0);
            anim.SetDuration(300);
            anim.Update += (o, eventArgs) =>
            {
                back.TranslationX = (float)eventArgs.Animation.AnimatedValue;
                front.TranslationX = (float)eventArgs.Animation.AnimatedValue - mFrontWidth;
            };
            anim.Start();
        }

        private void CloseMenu(View back, View front, float x)
        {
            var anim = ValueAnimator.OfFloat(x, mFrontWidth);
            anim.SetDuration(300);
            anim.Update += (o, eventArgs) =>
            {
                back.TranslationX = (float)eventArgs.Animation.AnimatedValue;
                front.TranslationX = (float)eventArgs.Animation.AnimatedValue - mFrontWidth;
            };
            anim.Start();
        }
    }
}