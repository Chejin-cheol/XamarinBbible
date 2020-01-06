using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinBible.Control;
using XamarinBible.Interface;
using XamarinBible.ViewModel.Base;

namespace XamarinBible.Behaviors
{
    public class SliderTouchDown : Behavior<PlatformSlider>
    {
        public static readonly BindableProperty AudioProperty = BindableProperty.Create(nameof(Audio), typeof(IAudio), typeof(SliderTouchDown), null );
        public IAudio Audio
        {
            get { return (IAudio)GetValue(AudioProperty); }
            set { SetValue(AudioProperty, value); }
        }

        protected override void OnAttachedTo(PlatformSlider bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TouchDown += TouchDown;
        }

        protected override void OnDetachingFrom(PlatformSlider bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TouchDown -= TouchDown;
        }

        public void TouchDown(Object sender ,EventArgs e)
        {
            Audio.Interrupt(true);
        }
    }
}
