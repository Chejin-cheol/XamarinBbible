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
using Xamarin.Forms.Platform.Android;
using XamarinBible.Control;
using XamarinBible.Droid.Renderer;

[assembly: ExportRenderer(typeof(PlatformSlider), typeof(Android_SermonSliderRenderer))]
namespace XamarinBible.Droid.Renderer
{
    public class Android_SermonSliderRenderer : SliderRenderer
    {
        public Android_SermonSliderRenderer(Context context) : base(context)
        {

        }
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

        }


        protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;
            if (Control != null)
            {
                Control.SetPadding(0, 0, 0, 0);

                // Cast your element here
                var element = (PlatformSlider)Element;

                var seekBar = Control;
                seekBar.StartTrackingTouch += (sender, args) =>
                {
                    element.TouchDownEvent(this, EventArgs.Empty);
                };

                seekBar.StopTrackingTouch += (sender, args) =>
                {

                    element.TouchUpEvent(this, EventArgs.Empty);
                };

                // On Android you need to check if ProgressChange by user
                seekBar.ProgressChanged += delegate (object sender, SeekBar.ProgressChangedEventArgs args)
                {
                    if (args.FromUser)
                        element.Value = (element.Minimum + ((element.Maximum - element.Minimum) * (args.Progress) / 1000.0));
                };
            }
        }
    }
}