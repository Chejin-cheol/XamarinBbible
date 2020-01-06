using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinBible.Control;
using XamarinBible.Droid.Renderer;

[assembly: ExportRenderer(typeof(SummaryView), typeof(Android_SummaryViewRenderer))]
namespace XamarinBible.Droid.Renderer
{
    public class Android_SummaryViewRenderer : LabelRenderer
    {
        public Android_SummaryViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if(e.PropertyName =="Text")
            {
 //               Element.Text = Element.Text.Replace(System.Environment.NewLine, "");
 //               Element.Text = Element.Text.Replace(" ", "\u00A0");
            }
        }
    }
}