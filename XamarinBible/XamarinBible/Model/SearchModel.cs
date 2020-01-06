using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinBible.Database;
using XamarinBible.Helper;
using XamarinBible.ViewModel;

namespace XamarinBible.Model
{
    public class SearchModel : BaseViewModel
    {
        private ObservableCollection<SearchResult> items = new ObservableCollection<SearchResult>();

        private SearchViewModel outerViewModel = null;
        public ICommand ItemClickCommand { get;  private set; }
        private int position;

        public SearchModel(SearchViewModel vm)
        {
            outerViewModel = vm;
            ItemClickCommand = new Command<string>(ItemClick);
        }


        public SearchViewModel OuterViewModel
        {
            get
            {
                return outerViewModel;
            }
        }

        public int Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value; 
            }
        }

        public ObservableCollection<SearchResult> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
                OnPropertyChanged("Items");
            }
        }

        public void DisposeViewModel()
        {
            Items.Clear();
            items = null;
        }
       
        private void ItemClick(string param)
        {
            int index = int.Parse(param);
            
            if( Position == 1 )
            {
                int book = outerViewModel.Book;
                int chapter = index;
                int[] items = BibleDataAccess.Instance().GetIndexCount(book, chapter);    // 0 chapter  count // 1 paragraph count
                outerViewModel.SearchPages[Position + 1].Items.Clear();
                outerViewModel.SearchPages[Position + 1].Items = null;
                outerViewModel.SearchPages[Position + 1].Items = outerViewModel.GetGridItem(items[1]);
                outerViewModel.Chapter = chapter ;
                outerViewModel.Position = Position + 1;
            }
            else if( Position == 2)
            {
                int book = outerViewModel.Book;
                int chapter = outerViewModel.Chapter;
                BibleDataAccess dataAccess = BibleDataAccess.Instance();
                Console.WriteLine(book + " / "+chapter +"  ** " );
                Console.WriteLine(GC.GetGeneration(outerViewModel.outerViewModel.Bible) + "  <===  generation  bible");
                outerViewModel.outerViewModel.Bible.Clear();
                outerViewModel.outerViewModel.Bible = null;
                outerViewModel.outerViewModel.Bible = new ObservableCollection<Bible>(dataAccess.SearchBibile(book, chapter));
                //outerViewModel.outerViewModel.BiblePageOpen = !outerViewModel.outerViewModel.BiblePageOpen;
                outerViewModel.outerViewModel.SearchPageOpen = !outerViewModel.outerViewModel.SearchPageOpen;
                outerViewModel.Position = 0;
                outerViewModel.outerViewModel.ScrollPosition = index - 1;
            }

//            GC.Collect();
        }
    }
}
