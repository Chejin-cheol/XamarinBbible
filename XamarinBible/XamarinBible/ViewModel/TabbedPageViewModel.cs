using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinBible.DI.Service;
using XamarinBible.Helper;

namespace XamarinBible.ViewModel
{
    public class TabbedPageViewModel : BaseViewModel
    {
        private string tabBackgroundColor , titleColor , pageBackgroundColor;
        private ISermonService _sermonService;

        public TabbedPageViewModel(ISermonService sermonService)
        {
            _sermonService = sermonService;
            if(Settings.LightMode)
            {
                TabBackgroundColor = "#ffffff";
                TitleColor = "#000000";
                PageBackgroundColor = "#ffffff";
            }
            else
            {
                TabBackgroundColor = "#282828";
                TitleColor = "#ffffff";
                PageBackgroundColor = "#343434";
            }
        }

        public string TabBackgroundColor
        {
            get
            {
                return tabBackgroundColor;
            }
            set
            {
                tabBackgroundColor = value;
                OnPropertyChanged("TabBackgroundColor");
            }
        }

        public string TitleColor
        {
            get
            {
                return titleColor;
            }
            set
            {
                titleColor = value;
                OnPropertyChanged("TitleColor");
            }
        }

        public string PageBackgroundColor
        {
            get
            {
                return pageBackgroundColor;
            }
            set
            {
                pageBackgroundColor = value;
                OnPropertyChanged("PageBackgroundColor");
            }
        }
    }
}
