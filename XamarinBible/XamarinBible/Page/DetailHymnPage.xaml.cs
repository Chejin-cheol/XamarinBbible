using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinBible.Interface;
using XamarinBible.ViewModel;

namespace XamarinBible.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailHymnPage : ContentPage
	{
		public DetailHymnPage ()
		{
			InitializeComponent ();
		}
        public void OnSearchActivated(Object sender , EventArgs e)
        {
            ((HymnViewModel)BindingContext).Position = 1;
        }
    }
}