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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinBible.Control;
using XamarinBible.Droid.Renderer;

[assembly: ExportRenderer(typeof(ShadowLine), typeof(Android_ShadowLineRenderer))]
namespace XamarinBible.Droid.Renderer
{
    class Android_ShadowLineRenderer : ViewRenderer
    {
        public Android_ShadowLineRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            var view  = new Android.Views.View(Context);
            view.Background = Context.GetDrawable(Resource.Drawable.shadow_down);
            
            SetNativeControl(view);
            base.OnElementChanged(e);
        }
    }
}