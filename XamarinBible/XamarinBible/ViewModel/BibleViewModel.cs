using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinBible.Database;
using XamarinBible.DI.Service;
using XamarinBible.Helper;
using XamarinBible.Model;
using XamarinBible.Page;

namespace XamarinBible.ViewModel
{
    public class BibleViewModel : DetailViewModel
    {
    
        SearchViewModel searchPageViewModel;
        public BibleDataAccess dataAccess;
        private ObservableCollection<Bible> bible;
        public static List<Bible> dashedBibles;
        private List<Colors> colors;

        private List<Language> languages = new List<Language>();
        private static string[] languageName = { "KOR" ,"NIV","KJV" ,"JPN","CHN" };

        private bool isSlideOpen, isSlideDownOpen ,isOptionMenuOpen = true ,biblePageOpen = true , searchPageOpen;
        private int scrollPosition = 0;
        private string cellTextColor, cellBackgroundColor;

        public ICommand NavigationBarCommand { get; private set; }
        public ICommand BibleSettingMenuCommand { get; private set; }
        public ICommand OptionMenuCommand { get; private set; }

        public BibleViewModel(TabbedPageViewModel rootViewModel)
        {
            MasterViewModel = rootViewModel;

            dataAccess = BibleDataAccess.Instance();
            dataAccess.TOP_CASE =  LanguageHelper.GetTopVersion( Settings.Languages.Split(',') );

            bible = new ObservableCollection<Bible>(dataAccess.GetBible(BibleDataAccess.Mode.Init));
            dashedBibles = new List<Bible>();

            colors = dataAccess.GetColors();
            searchPageViewModel = new SearchViewModel(this);

            for (int i = 0; i < languageName.Length; i++)
            { 
                Language languase = new Language()
                {
                    Kind = languageName[i],
                    IsChecked = Settings.Languages.Contains( languageName[i] )
                };
                languages.Add(languase);
            }

            NavigationBarCommand = new Command<string>(NavigationBarChanged);
            BibleSettingMenuCommand = new Command<string>(BibleSettingAction);
            OptionMenuCommand = new Command<string>(OptionMenuAction);

            if (Settings.LightMode)
            {
                CellTextColor = "#000000";
                CellBackgroundColor = "#ffffff";
                searchPageViewModel.TextColor = "#000000";
                searchPageViewModel.BackgroundColor = "#ffffff";
            }
            else
            {
                CellTextColor = "#ffffff";
                CellBackgroundColor = "#343434";
                searchPageViewModel.TextColor = "#fffffff";
                searchPageViewModel.BackgroundColor = "#343434";
            }
        }

        //current
        public BibleDataAccess DataAccess
        {
            private set { }
            get
            {
                return dataAccess;
            }
        }

        public ObservableCollection<Bible> Bible
        {
            get
            {
                return bible;
            }
            set
            {
                bible = value;
                OnPropertyChanged("Bible");
            }
        }
        public Object DashedBibles
        {
            set
            {
                var bible = ((Bible)value);
                if (bible.dash)          
                {
                    if (!SlideOpen)
                    {
                        SlideOpen = !SlideOpen;
                    }
                    dashedBibles.Add((Bible)value);
                }
                else
                {
                    dashedBibles.Remove((Bible)value);
                    if(dashedBibles.Count == 0)
                    {
                        SlideOpen = false;
                    }
                }
            }

            get
            {
                return dashedBibles;
            }
        }

        public List<Language> Languages
        {
            get
            {
                return languages;
            }
            set
            {
                languages = value;
                OnPropertyChanged("Languages");
            }
        }

        public List<Colors> Colors
        {
            get
            {
                return colors;
            }
            set
            {
                colors = value;
                OnPropertyChanged("Colors");
            }
        }

        public bool OptionMenuOpen
        {
            get
            {
                return isOptionMenuOpen;
            }

            set
            {
                isOptionMenuOpen = value;
                OnPropertyChanged("OptionMenuOpen");
            }
        }

        public bool SlideOpen
        {
            get
            {
                return isSlideOpen;
            }
            set
            {
                isSlideOpen = value;
                OnPropertyChanged("SlideOpen");
            }
        }

        public bool SlideDownOpen
        {
            get
            {
                return isSlideDownOpen;
            }
            set
            {
                isSlideDownOpen = value;
                OnPropertyChanged("SlideDownOpen");
            }
        }

        public double FontSize
        {
            set
            {
                Settings.FontSize = value;
                OnPropertyChanged("FontSize");
            }
            get
            {
                return Settings.FontSize;
            }
        }

        public string CellTextColor
        {
            set
            {
                cellTextColor = value;
                OnPropertyChanged("CellTextColor");
            }
            get
            {
                return cellTextColor;
            }
        }

        public string CellBackgroundColor
        {
            set
            {        
                cellBackgroundColor = value;
                OnPropertyChanged("CellBackgroundColor");
            }
            get
            {
                return cellBackgroundColor;
            }
        }


        public int ScrollPosition
        {
            set
            {
                scrollPosition = value;
                OnPropertyChanged("ScrollPosition");
            }
            get
            {
                return scrollPosition;
            }
        }


        public bool BiblePageOpen
        {
            set
            {
                biblePageOpen = value;
                OnPropertyChanged("BiblePageOpen");
            }
            get
            {
                return biblePageOpen;
            }
        }
        public bool SearchPageOpen
        {
            set
            {
                searchPageOpen = value;
                OnPropertyChanged("SearchPageOpen");
            }
            get
            {
                return searchPageOpen;
            }
        }

        public SearchViewModel SearchPageViewModel
        {
            get
            {
                return searchPageViewModel;
            }

            private set
            {
                searchPageViewModel = value;
            }
        }

        //command method

        private void NavigationBarChanged(String sender)
        {
            switch (sender)
            {
                case "search":

                    /*
                    ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex(MasterViewModel.TabBackgroundColor);
                    ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.FromHex(MasterViewModel.TitleColor);
                    App.Current.MainPage.Navigation.PushAsync(new SearchBiblePage() { BackgroundColor = Color.FromHex(CellBackgroundColor) } );
                    */
                    SearchPageOpen = ! SearchPageOpen;
                  
                    break;

                case "setting":
                    SlideDownOpen = ! SlideDownOpen;
                    break;
            }
        }

        private void OptionMenuAction(String sender)
        {
            switch (sender)
            {
                case "next":
                    var nextList =  dataAccess.GetBible(BibleDataAccess.Mode.Next);
                    if( nextList != null )
                    {
                        var next = new ObservableCollection<Bible>(nextList);
                        Bible.Clear();
                        Bible = next;
                    }
                    break;

                case "prev":                    
                    var prevList = dataAccess.GetBible(BibleDataAccess.Mode.Prev);
                    if(prevList != null)
                    {
                        var prev = new ObservableCollection<Bible>(prevList);
                        Bible.Clear();
                        Bible = prev;
                    }
                    break;

                case "note":
                    break;

                case "sermon":
                    break;
            }
        }

        private void BibleSettingAction(string param)
        {
            switch(param)
            {
                case "display":
                    bool mode = Settings.LightMode = !Settings.LightMode;
                    if (mode)
                    {
                        CellTextColor = "#000000";
                        CellBackgroundColor = "#ffffff";
                        MasterViewModel.TabBackgroundColor = "#ffffff";
                        MasterViewModel.TitleColor = "#000000";
                        searchPageViewModel.TextColor = "#000000";
                        searchPageViewModel.BackgroundColor = "#ffffff";
                    }
                    else
                    {
                        CellTextColor = "#ffffff";
                        CellBackgroundColor = "#343434";
                        MasterViewModel.TabBackgroundColor = "#282828";
                        MasterViewModel.TitleColor = "#ffffff";
                        searchPageViewModel.TextColor = "#ffffff";
                        searchPageViewModel.BackgroundColor = "#343434";
                    }
                 break;
                case "font_up":
                    FontSize += 1;
                    break;
                case "font_down":
                    FontSize -= 1;
                    break;
            }
        }

    }
}
