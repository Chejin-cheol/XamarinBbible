using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinBible.Control;
using XamarinBible.iOS.Renderer;

[assembly: ExportRenderer(typeof(PlatformSlider), typeof(IOS_SliderRenderer))]
namespace XamarinBible.iOS.Renderer
{
    public class IOS_SliderRenderer : SliderRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.TouchDown += Control_TouchDown;
                Control.TouchUpInside += Control_TouchUpInside;
                Control.TouchUpOutside += Control_TouchUpOutside;
            }
        }
        private void Control_TouchDown(object sender, EventArgs e)
        {
            var slider = Element as PlatformSlider;
            slider.TouchDownEvent(this, EventArgs.Empty);
        }

        private void Control_TouchUpInside(object sender, EventArgs e)
        {

            Console.WriteLine("업업2");
            var slider = Element as PlatformSlider;
            slider.TouchUpEvent(this, EventArgs.Empty);
        }

        private void Control_TouchUpOutside(object sender, EventArgs e)
        {
            Console.WriteLine("업업");
            var slider = Element as PlatformSlider;
            slider.TouchUpEvent(this, EventArgs.Empty);
        }
    }
}