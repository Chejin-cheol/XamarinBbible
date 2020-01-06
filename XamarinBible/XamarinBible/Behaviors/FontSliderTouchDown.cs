using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinBible.Control;
using XamarinBible.ViewModel;

namespace XamarinBible.Behaviors
{
    public class FontSliderTouchDown : Behavior<PlatformSlider>
    {
        protected override void OnAttachedTo(PlatformSlider slider)
        {
            base.OnAttachedTo(slider);
        }
        protected override void OnDetachingFrom(PlatformSlider slider)
        {
            base.OnDetachingFrom(slider);
        }

        public void TouchUp(object sender, EventArgs e)
        {
            
        }
    }
}
