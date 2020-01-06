using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinBible.Control;
using XamarinBible.Interface;
using XamarinBible.ViewModel.Base;

namespace XamarinBible.Behaviors
{
    public class SliderTouchUp : Behavior<PlatformSlider>
    {
        public static readonly BindableProperty AudioProperty = BindableProperty.Create(nameof(Audio), typeof(IAudio), typeof(SliderTouchUp), null);
        public IAudio Audio
        {
            get { return (IAudio)GetValue(AudioProperty); }
            set { SetValue(AudioProperty, value); }
        }

        protected override void OnAttachedTo(PlatformSlider bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TouchUp += TouchUp;
        }

        protected override void OnDetachingFrom(PlatformSlider bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TouchUp -= TouchUp;
        }

        public void TouchUp(Object sender, EventArgs e)
        {
            Audio.SeekTo(((Slider)sender).Value);
            Audio.Interrupt(false);
        }
    }
}
