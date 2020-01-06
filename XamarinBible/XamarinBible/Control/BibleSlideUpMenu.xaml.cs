using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinBible.Control
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BibleSlideUpMenu : ContentView
	{
        
        public BibleSlideUpMenu()
        {
            InitializeComponent();
        }

        public double translateHeight = 0;

        public static readonly BindableProperty SlideOpenProperty = BindableProperty.Create(nameof(SlideOpen), typeof(bool), typeof(BibleSlideUpMenu), false, propertyChanged: StatusChaged);
        public bool SlideOpen
        {
            get { return (bool)GetValue(SlideOpenProperty); }
            set { SetValue(SlideOpenProperty, value); }
        }

        private static void StatusChaged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null)
            {
                var slider = ((BibleSlideUpMenu)bindable);
                
                if ((bool)newValue)
                {                    
                    slider.TranslateTo(0, -slider.translateHeight, 250, Easing.SinInOut);
                }
                else
                {
                    slider.TranslateTo(0, slider.translateHeight, 250, Easing.SinInOut);
                }
            }
        }
    }
}