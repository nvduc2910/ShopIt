using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.View.Menu;
using Android.Views;
using Android.Widget;
using MvvmCross.Platform;
using MvvmCross.Platform.Core;
using ShopIt.Core.Services;

namespace ShopIt.Droid.Services
{
    public class MessageboxService : IMessageboxService
    {
        public void Show(string title, string message)
        {
            var context = Mvx.Resolve<IDroidService>().CurrentContext;
            var dialogBuider = new AlertDialog.Builder(context);
            dialogBuider.SetTitle(title);
            dialogBuider.SetMessage(message);
            dialogBuider.Create().Show();
        }

        public Task<bool> ShowTwoOptions(string title, string message, string cancelOption, string yesOption)
        {
            var tcs = new TaskCompletionSource<bool>();
            var context = Mvx.Resolve<IDroidService>().CurrentContext;
            var dialogBuider = new AlertDialog.Builder(context);
            dialogBuider.SetTitle(title);
            dialogBuider.SetMessage(message);
            dialogBuider.SetNegativeButton(cancelOption, (sender, args) => tcs.SetResult(false));
            dialogBuider.SetPositiveButton(yesOption, (sender, args) => tcs.SetResult(true));
            dialogBuider.Create().Show();
            return tcs.Task;
        }

        public Task<int> ShowThreeOptions(string title, string message, string cancelOption, string firstOption, string secondOption)
        {
            var tcs = new TaskCompletionSource<int>();
            var context = Mvx.Resolve<IDroidService>().CurrentContext;
            var dialogBuider = new AlertDialog.Builder(context);
            dialogBuider.SetTitle(title);
            dialogBuider.SetMessage(message);
            dialogBuider.SetNegativeButton(cancelOption, (sender, args) => tcs.SetResult(0));
            dialogBuider.SetNeutralButton(firstOption, (sender, args) => tcs.SetResult(1));
            dialogBuider.SetPositiveButton(secondOption, (sender, args) => tcs.SetResult(2));
            dialogBuider.Create().Show();
            return tcs.Task;
        }

        public Task<int> ShowOptions(string title, string message, MessageboxShowType showType, params MessageboxOption[] options)
        {
            
            var tcs = new TaskCompletionSource<int>();
            var context = Mvx.Resolve<IDroidService>().CurrentContext;
            AlertDialog dialog = null;

            LayoutInflater _inflatorservice = (LayoutInflater) context.GetSystemService(Context.LayoutInflaterService);
            LinearLayout layout = _inflatorservice.Inflate(Resource.Layout.MessageBox, null) as LinearLayout;

            TextView tvTitle = layout.FindViewById<TextView>(Resource.Id.title);
            TextView tvMessage = layout.FindViewById<TextView>(Resource.Id.message);
            TextView tvBtn1 = layout.FindViewById<TextView>(Resource.Id.btn1);
            TextView tvBtn2 = layout.FindViewById<TextView>(Resource.Id.btn2);
            TextView tvBtn3 = layout.FindViewById<TextView>(Resource.Id.btn3);
            TextView tvBtn4 = layout.FindViewById<TextView>(Resource.Id.btn4);

            tvTitle.SetText(title, TextView.BufferType.Spannable);
            tvMessage.SetText(message,TextView.BufferType.Spannable);

            tvBtn1.SetText(options[1].Text,TextView.BufferType.Spannable);
            tvBtn2.SetText(options[2].Text, TextView.BufferType.Spannable);
            tvBtn3.SetText(options[3].Text, TextView.BufferType.Spannable);
            tvBtn4.SetText(options[0].Text, TextView.BufferType.Spannable);

            tvBtn1.Click += (sender, args) =>
            {
                if(dialog!=null) dialog.Hide();
                tcs.SetResult(1);
            };
            tvBtn2.Click += (sender, args) =>
            {
                if (dialog != null) dialog.Hide();
                tcs.SetResult(2);
            };
            tvBtn3.Click += (sender, args) =>
            {
                if (dialog != null) dialog.Hide();
                tcs.SetResult(3);
            };
            tvBtn4.Click += (sender, args) =>
            {
                if (dialog != null) dialog.Hide();
                tcs.SetResult(0);
            };
         
            var dialogBuider = new AlertDialog.Builder(context);
            dialogBuider.SetView(layout);

            dialog = dialogBuider.Create();
            dialog.Show();

            return tcs.Task;
        }

        public void ShowToast(string message, int timeoutMs = 1500)
        {
            var context = (Activity)Mvx.Resolve<IDroidService>().CurrentContext;
            Toast.MakeText(context, message, ToastLength.Short).Show();
        }
    }
}