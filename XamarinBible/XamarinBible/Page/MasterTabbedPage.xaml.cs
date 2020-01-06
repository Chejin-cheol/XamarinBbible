using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using XamarinBible.ViewModel;

namespace XamarinBible.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterTabbedPage : Xamarin.Forms.TabbedPage
	{
        NavigationPage nav;
        ContentPage[] contents;
        public MasterTabbedPage ()
		{
			InitializeComponent();
            NavigationPage.SetHasNavigationBar(this,false);
            if(Device.RuntimePlatform == Device.Android)
            {
                On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
                On<Xamarin.Forms.PlatformConfiguration.Android>().SetIsSwipePagingEnabled(false);
                On<Xamarin.Forms.PlatformConfiguration.Android>().SetIsSmoothScrollEnabled(false);
                On<Xamarin.Forms.PlatformConfiguration.Android>().SetBarSelectedItemColor(Color.Black);
            }
        }
	}
}