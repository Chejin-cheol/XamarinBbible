using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinBible.Control
{
    public partial class Indicator : BoxView
    {
        public static readonly BindableProperty DirectionProperty = BindableProperty.Create(nameof(Direction), typeof(int), typeof(Indicator), 0, propertyChanged: DirectionChange);
        public int Direction
        {
            get { return (int)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }


        public Indicator()
        {

        }


        private static void DirectionChange(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null)
            {
                var indicator = ((Indicator)bindable);
                int direction = (int)newValue;

                if (direction > 0)
                {
                    indicator.TranslateTo(indicator.Width * direction, 0, 250, Easing.SinInOut);
                }
                else
                {
                    indicator.TranslateTo(-indicator.Width * direction, 0, 250, Easing.SinInOut);
                }
            }
        }

    }
}
