using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinBible.Control;
using XamarinBible.Database;
using XamarinBible.Helper;
using XamarinBible.Model;

namespace XamarinBible.ViewModel
{
    public class SearchViewModel : BaseViewModel
    {
        public BibleViewModel outerViewModel = null;
        private ObservableCollection<SearchModel> searchPages { get; set; }
        private BibleDataAccess dataAccess = null;
        private string textColor ,backgroundColor;
        private int book, chapter;
        private int postion;
        private int directionScale;
        
        public ICommand PageChangedCommand { get; private set; }

        public SearchViewModel(BibleViewModel vm)
        {
            outerViewModel = vm;
            dataAccess = BibleDataAccess.Instance();
            SearchPages = new ObservableCollection<SearchModel>();
            PageChangedCommand = new Command<int>(PageChanged);

            BookMark  bookMark = dataAccess.GetBookMark();
            Book = bookMark.book;
            Chapter = bookMark.chapter;
            int[] indexInfo = dataAccess.GetIndexCount( bookMark.book , bookMark.chapter );


            for (int i = 0; i < 3; i++)
            {

                SearchModel model = new SearchModel(this);
                if (i >= 1)
                {
                    model.Items = GetGridItem( indexInfo[i-1] );
                    model.Position = i;
                }
                else
                {
                    model.Items = dataAccess.GetBibleNames();
                    model.Position = i;
                }
                SearchPages.Add(model);
            }

        }

        public ObservableCollection<SearchModel> SearchPages
        {
            get
            {
                return searchPages;
            }
            set
            {
                searchPages = value;
            }
        }

        public double IndicatorHeight
        {
            get
            {
                return App.screenWidth / 3;
            }
        }

        public int Position
        {
            get
            {
                return postion;
            }
            set
            {
                if ((value - postion) >0 )
                {
                    DirectionScale = value;
                }
                else
                {
                    DirectionScale = -value;
                }

                postion = value;
                OnPropertyChanged("Position");
            }
        }

        public int DirectionScale
        {
            get
            {
                return directionScale;
            }
            set
            {
                directionScale = value;
                OnPropertyChanged("DirectionScale");
            }
        }


        public int Book
        {
            get => book;
            set => book = value;
        }
        public int Chapter
        {
            get => chapter;
            set => chapter = value;
        }
        
        public ObservableCollection<SearchResult> GetGridItem(int counts)
        {
            ObservableCollection<SearchResult> girdList = new ObservableCollection<SearchResult>();
            for(int i=1; i<= counts; i++)
            {
                SearchResult item = new SearchResult(); 
                item.Value = i.ToString();
                girdList.Add(item);
            }
            return girdList;
        }
        
        public void DisposeViewModel()
        {
            foreach (SearchModel itemViewModel in SearchPages)
            {
                itemViewModel.DisposeViewModel();
            }
        }

        private void PageChanged(int index )
        {
            Position = index;
        }

        public string TextColor
        {
            set
            {
                textColor = value;
                OnPropertyChanged("TextColor");
            }
            get
            {
                return textColor;
            }
        }

        public string BackgroundColor
        {
            set
            {
                backgroundColor = value;
                OnPropertyChanged("BackgroundColor");
            }
            get
            {
                return backgroundColor;
            }
        }

    }
}
