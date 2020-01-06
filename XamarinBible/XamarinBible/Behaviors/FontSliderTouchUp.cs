using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinBible.Control;

namespace XamarinBible.Behaviors
{
    public class FontSliderTouchUp : Behavior<PlatformSlider>
    {
        protected override void OnAttachedTo(PlatformSlider slider)
        {
            base.OnAttachedTo(slider);
        }
        protected override void OnDetachingFrom(PlatformSlider slider)
        {
            base.OnDetachingFrom(slider);
        }

        public void TouchDown(Object sender , EventArgs e)
        {

        }
    }
}
