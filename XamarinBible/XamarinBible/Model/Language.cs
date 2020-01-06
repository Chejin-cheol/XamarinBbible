using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinBible.Database;
using XamarinBible.Helper;
using XamarinBible.ViewModel;

namespace XamarinBible.Model
{
    public class Language : INotifyPropertyChanged
    {
        private bool isChecked = false;
        private string kind , checkedColor;

        public ICommand LanguageChanged { get; private set; }

        public Language()
        {
            LanguageChanged = new Command<Object>(LanguageChangeCommand);
        }
        
        public string Kind
        {
            get
            {
                return kind;
            }
            set
            {
                kind = value;
                OnPropertyChanged("Kind");
            }
        }

        public bool IsChecked
        {
            get   
            {
                return isChecked;
            }
            set
            {
                string[] langs = Settings.Languages.Split(',');
                string quotKind = "'" + Kind + "'";
                if ( langs.Length == 1 && langs[0].Equals( quotKind ))
                {
                    CheckedColor = "#777777";
                    isChecked = true;
                    OnPropertyChanged("IsChecked");
                    return;
                }
                isChecked = value;
                
                if ( isChecked )
                {
                    CheckedColor = "#777777";
                    Settings.Languages = Settings.Languages +","+ quotKind;
                }
                else
                {
                    CheckedColor = "#cccccc";            
                    int position = Settings.Languages.IndexOf(quotKind);
                    
                    if (position != 0)
                    {
                        quotKind = "," + quotKind;
                    }
                    else
                    {
                        int lastPosition = Settings.Languages.LastIndexOf(quotKind);
                        quotKind = Settings.Languages[ Kind.Length + 2 ] == ',' ? quotKind + "," : quotKind;  
                    }
                    Settings.Languages = Settings.Languages.Replace(quotKind, "");
                }
                Console.WriteLine(Settings.Languages);
                OnPropertyChanged("IsChecked");
            }
        }


        public string CheckedColor
        {
            get
            {
                return checkedColor;
            }
            set
            {
                checkedColor = value;
                OnPropertyChanged("CheckedColor");
            }
        }


        private void LanguageChangeCommand(object param)
        {
            IsChecked = ! IsChecked;
            BibleViewModel parentVM = (BibleViewModel)param;
            parentVM.Bible.Clear();
            parentVM.dataAccess.TOP_CASE = LanguageHelper.GetTopVersion(Settings.Languages.Split(','));
            parentVM.Bible = new ObservableCollection<Bible>( BibleDataAccess.Instance().GetBible(BibleDataAccess.Mode.Init ) );
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
