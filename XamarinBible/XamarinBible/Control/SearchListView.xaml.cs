using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinBible.Database;
using XamarinBible.Model;
using XamarinBible.ViewModel;

namespace XamarinBible.Control
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchListView : ListView
	{
		public SearchListView ()
		{
			InitializeComponent ();
            ItemSelected += OnItemSelected;
        }

        private void OnItemSelected(Object sender , SelectedItemChangedEventArgs e)
        {
            SearchViewModel vm = Parent.BindingContext as SearchViewModel;

            int[] index = BibleDataAccess.Instance().GetIndexCount( e.SelectedItemIndex + 1 );
            ObservableCollection<SearchResult> items = new ObservableCollection<SearchResult>();

            for(int i=0; i< index.Length;i++)
            {
                vm.SearchPages[i + 1].Items.Clear();
                vm.SearchPages[i + 1].Items = null;
                vm.SearchPages[i + 1].Items = vm.GetGridItem(index[i]);
            }
            vm.Book = e.SelectedItemIndex + 1;
            vm.Position = vm.Position+1;
        }
	}
}