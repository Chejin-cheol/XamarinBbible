using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinBible.Control
{
    public class SermonTabView : Label
    {
        public static readonly BindableProperty IsTabbedProperty = BindableProperty.Create(nameof(IsTabbed), typeof(bool), typeof(SermonTabView), false, propertyChanged: StatusChaged);
        public bool IsTabbed
        {
            get { return (bool)GetValue(IsTabbedProperty); }
            set { SetValue(IsTabbedProperty, value); }
        }

        private static void StatusChaged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null)
            {
                var view = ((SermonTabView)bindable);

                if ((bool)newValue)
                {
                    view.ScaleTo(1.3);
                }
                else
                {
                    view.ScaleTo(1);
                }
            }
        }
    }
}
