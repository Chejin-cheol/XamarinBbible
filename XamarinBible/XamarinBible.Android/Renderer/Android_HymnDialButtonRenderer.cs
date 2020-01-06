using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinBible.Control;
using XamarinBible.Droid.Renderer;


[assembly: ExportRenderer(typeof(HymnDialButton), typeof(Android_HymnDialButtonRenderer))]
namespace XamarinBible.Droid.Renderer
{

    public class Android_HymnDialButtonRenderer : ImageButtonRenderer
    {
        public Android_HymnDialButtonRenderer(Context context) : base(context)
        {
            Console.WriteLine("이미지:");
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ImageButton> e)
        {
            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }
    }
}