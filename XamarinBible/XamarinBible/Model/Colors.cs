using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinBible.ViewModel;

namespace XamarinBible.Model
{
    public class Colors : BaseViewModel
    {
        private string color;
        public ICommand ColorSelectCommand { private set; get; }

        public Colors()
        {
            ColorSelectCommand = new Command<Object>(ColorSelected);
        }

        public string Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                OnPropertyChanged("Color");                   
            }
        }

        private void ColorSelected(Object param)
        {
            var bibleViewModel = param as BibleViewModel;
            var dashList = (List<Bible>)bibleViewModel.DashedBibles;

            for(int i=0; i< dashList.Count; i++)
            {
                bibleViewModel.DataAccess.MarkHighlight(dashList[i].id , Color);
                dashList[i].Highlight = Color;
                dashList[i].dash = false;
            }
            dashList.Clear();
            bibleViewModel.SlideOpen = false;
        }
    }
}
