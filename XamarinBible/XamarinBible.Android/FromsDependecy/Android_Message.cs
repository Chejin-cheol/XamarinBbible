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
using Xamarin.Forms;
using XamarinBible.Droid.FromsDependecy;
using XamarinBible.Interface;

[assembly: Dependency(typeof(Android_Message))]
namespace XamarinBible.Droid.FromsDependecy
{
    class Android_Message : IMessage
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(MainActivity._context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(MainActivity._context, message, ToastLength.Short).Show();
        }
    }
}