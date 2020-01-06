using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinBible.Control;
using XamarinBible.Helper;
using XamarinBible.ViewModel;

namespace XamarinBible.Model
{
    public class Bible : INotifyPropertyChanged
    {

        private string _sentence;
        private bool _dash = false;
        private string highlight;
        public int id { get; set; }
        public int book { get; set; }
        public int chapter { get; set; }
        public int paragraph { get; set; }

        public ICommand LabelCommand { get; private set; }

        public Bible()
        {
            LabelCommand = new Xamarin.Forms.Command(OnLabelClicked);
        }

        public string sentence
        {
            get
            {
                return _sentence;
            }
            set
            {
                _sentence = value;
                OnPropertyChanged("sentence");
            }
        }
        public string para { get; set; }

        public bool dash
        {
            get
            {
                return _dash;
            }
            set
            {
                _dash = value;
                OnPropertyChanged("dash");
            }
        }
        public string Highlight
        {
            get
            {
                return highlight;
            }
            set
            {
                highlight = value;
                OnPropertyChanged("Highlight");
            }
        }


        public double FontSize
        {
            get
            {
                return Settings.FontSize;
            }
        }

        public int CellSpace
        {
            get
            {
                return Settings.CellSpace;
            }
        }

       public void OnLabelClicked(Object param)
        {
            
            var parentVM = (param as BibleViewModel);
            if (parentVM != null)
            {
                dash = !dash;
                parentVM.DashedBibles = this;
            }
        }
    


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
