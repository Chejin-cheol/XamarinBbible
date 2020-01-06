using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinBible.Model;

namespace XamarinBible.Control
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HymnDialView : ContentView
	{
		public HymnDialView ()
		{
			InitializeComponent ();
		}

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            var padding = width * 0.03;
            dial.Padding = new Thickness(padding, 0, padding, 0);
            dial.ColumnSpacing = padding;
            dial.RowSpacing = padding;

            number.FontSize = page.FontSize = height * 0.07;
        }
    }
}