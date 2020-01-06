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
using XamarinBible.Droid.Widget;
using XamarinBible.Interface;


[assembly: Xamarin.Forms.Dependency(typeof(Android_NativePage))]
namespace XamarinBible.Droid.FromsDependecy
{
    class Android_NativePage : INativePage
    {

        public void StartPage(string category ,string fileName, string page)
        {
            var intent = new Intent( MainActivity._context , typeof(HymnActivity));
            intent.PutExtra("page", page);
            intent.PutExtra("category", category);
            intent.PutExtra("fileName",fileName);
            intent.AddFlags(ActivityFlags.ClearTop);
            MainActivity._context.StartActivity(intent);
        }
    }
}