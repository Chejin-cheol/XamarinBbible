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
    public partial class BibleOptionMenu : ContentView
    {
        public BibleOptionMenu()
        {
            InitializeComponent();
        }


        public double translateHeight = 0;
        public static readonly BindableProperty OpenProperty = BindableProperty.Create(nameof(Open), typeof(bool), typeof(BibleOptionMenu), false, propertyChanged: StatusChaged);
        public bool Open
        {
            get { return (bool)GetValue(OpenProperty); }
            set { SetValue(OpenProperty, value); }
        }

        private static void StatusChaged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null)
            {
                Console.WriteLine("뭐냐"+ (bool)newValue);
                var slider = ((BibleOptionMenu)bindable);

                if ((bool)newValue)
                {
                    slider.TranslateTo(0, 0, 250, Easing.SinInOut);
                }
                else
                {
                    slider.TranslateTo(0, slider.translateHeight, 250, Easing.SinInOut);
                }
            }
        }

    }
}