using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinBible.Control;
using XamarinBible.Model;
using XamarinBible.ViewModel;

namespace XamarinBible.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailBiblePage : ContentPage
	{
        BibleViewModel viewModel;
		public DetailBiblePage ()
		{
            InitializeComponent();
            viewModel = (BibleViewModel)BindingContext;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            
            sliderMenu.translateHeight = height * 0.25;
            AbsoluteLayout.SetLayoutBounds(sliderMenu, new Rectangle(0, height, width, sliderMenu.translateHeight));
            AbsoluteLayout.SetLayoutFlags(sliderMenu, AbsoluteLayoutFlags.None);

            slideDownMenu.translateHeight = height * 0.4;
            AbsoluteLayout.SetLayoutBounds(slideDownMenu, new Rectangle(0, - slideDownMenu.translateHeight, width, slideDownMenu.translateHeight));
            AbsoluteLayout.SetLayoutFlags(slideDownMenu, AbsoluteLayoutFlags.None);


            optionMeun.translateHeight = height * 0.1;
            AbsoluteLayout.SetLayoutBounds(optionMeun, new Rectangle(0, height - optionMeun.translateHeight, width, optionMeun.translateHeight));
            AbsoluteLayout.SetLayoutFlags(optionMeun, AbsoluteLayoutFlags.None);
        }

    }
}