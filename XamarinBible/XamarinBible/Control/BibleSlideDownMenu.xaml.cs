using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinBible.Helper;
using XamarinBible.ViewModel;

namespace XamarinBible.Control
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BibleSlideDownMenu : ContentView
	{
		public BibleSlideDownMenu ()
		{
			InitializeComponent ();
           

            int defaultSize = 0;

            defaultSize = (int)Settings.FontSize;
            int gap = defaultSize / 2;

/*
            fontSlider.Maximum = defaultSize + gap;
            fontSlider.Minimum = defaultSize - gap;
            fontSlider.ValueChanged += FontChanged;
*/
        }

        public double translateHeight = 0;
        public static readonly BindableProperty SlideDownOpenProperty = BindableProperty.Create(nameof(SlideDownOpen), typeof(bool), typeof(BibleSlideDownMenu), false, propertyChanged: StatusChaged);
        public bool SlideDownOpen
        {
            get { return (bool)GetValue(SlideDownOpenProperty); }
            set { SetValue(SlideDownOpenProperty, value); }
        }

        private static void StatusChaged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null)
            {
                var slider = ((BibleSlideDownMenu)bindable);

                if ((bool)newValue)
                {
                    slider.TranslateTo(0, slider.translateHeight, 250, Easing.SinInOut);
                }
                else
                {
                    slider.TranslateTo(0, -slider.translateHeight, 300, Easing.SinInOut);
                }
            }
        }
    }
}