using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinBible.Database;
using XamarinBible.Interface;
using XamarinBible.ViewModel;

namespace XamarinBible.Model
{
    public class HymnDialModel : HymnModel
    {
        private string number="";
        
        public HymnDialModel(HymnViewModel vm) : base(vm)
        {
            OuterViewModel = vm;
            ItemClickCommand = new Command<string>(ItemClick);
            
        }

        private void ItemClick(string param)
        {
            switch(param)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    if(Number.Length <3)
                    {
                        Number = Number + param;
                    }
                    break;
                case "x":
                    if (Number.Length >0)
                    {
                        Number = Number.Substring(0 , Number.Length -1);
                    }
                    break;
                case "check":
                    var page  =  HymnDataAccess.Instance().GetPage(Number);
                    if(page != -1)
                    {                        
                        string category = HymnDataAccess.Instance().GetCategory();
                        string fileName = HymnDataAccess.Instance().GetFileName();
                        DependencyService.Get<IAudio>().Close();
                        DependencyService.Get<INativePage>().StartPage(category, fileName, page.ToString());
                    }

                    break;
            }
        }

        public string Number
        {
            get => number;
            set{
                number = value;
                OnPropertyChanged("Number");
            }
        }
    }
}
