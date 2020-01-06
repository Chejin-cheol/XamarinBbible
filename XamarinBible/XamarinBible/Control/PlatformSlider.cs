using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinBible.Control
{
    public class PlatformSlider : Slider
    {
        // Events for external use (for example XAML)
        public event EventHandler TouchDown;
        public event EventHandler TouchUp;
        
        // Events called by renderers
        public EventHandler TouchDownEvent;
        public EventHandler TouchUpEvent;
        
        public PlatformSlider()
        {
            TouchDownEvent = delegate
            {
                TouchDown?.Invoke(this, EventArgs.Empty);
            };
            TouchUpEvent = delegate
            {
                TouchUp?.Invoke(this, EventArgs.Empty);
            };
        }
    }
}
