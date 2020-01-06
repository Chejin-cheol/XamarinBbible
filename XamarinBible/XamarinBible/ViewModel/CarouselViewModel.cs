using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinBible.Helper;
using XamarinBible.ViewModel;

namespace XamarinBible.ViewModel
{
    public class CarouselViewModel : BaseViewModel
    {
        private SearchViewModel outerViewModel = null;
        private int position;
        private string textColor, backgroundColor;
        private int directionScale;
        private int tabCount;

        public ICommand PageChangedCommand { get; private set; }

        public CarouselViewModel()
        {
            PageChangedCommand = new Command<int>(PageChanged);

            if (Settings.LightMode)
            {
                TextColor = "#000000";
                BackgroundColor = "#ffffff";
            }
            else
            {
                TextColor = "#ffffff";
                BackgroundColor = "#343434";
            }
        }
        public CarouselViewModel(BaseViewModel outerViewModel)
        {
            PageChangedCommand = new Command<int>(PageChanged);

            if (Settings.LightMode)
            {
                TextColor = "#000000";
                BackgroundColor = "#ffffff";
            }
            else
            {
                TextColor = "#ffffff";
                BackgroundColor = "#343434";
            }
        }

        public int TabCount
        {
            get => tabCount;
            set => tabCount = value;
        }

        public double IndicatorHeight
        {
            get
            {
                return App.screenWidth / tabCount;
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
                if ((value - position) > 0)
                {
                    DirectionScale = value;
                }
                else
                {
                    DirectionScale = -value;
                }

                position = value;
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
        
        private void PageChanged(int index)
        {
            Position = index;
        }

    }
}
