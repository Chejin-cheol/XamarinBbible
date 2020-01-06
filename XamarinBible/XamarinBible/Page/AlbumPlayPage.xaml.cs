using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinBible.ViewModel;

namespace XamarinBible.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AlbumPlayPage : ContentPage
	{
		public AlbumPlayPage(string groupName , int groupId)
		{
			InitializeComponent ();
            ((AlbumPlayViewModel)BindingContext).SetPlayList(groupName, groupId);
		}

        protected override void OnDisappearing()
        {
            ((AlbumPlayViewModel)BindingContext).Distroy();
            base.OnDisappearing();
        }
         
    }
}