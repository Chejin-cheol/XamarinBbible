using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinBible.Database;
using XamarinBible.Interface;
using XamarinBible.ViewModel;

namespace XamarinBible.Model
{
    public class HymnListModel : HymnModel
    {
        private ObservableCollection<Hymn> items = new ObservableCollection<Hymn>();

        public HymnListModel(HymnViewModel vm) : base(vm)
        {
            OuterViewModel = vm;
            ItemClickCommand = new Command<int>(ItemClick);
        }


        public ObservableCollection<Hymn> Items
        {
            get
            {
                return items;
            }
            set
            {
                items.Clear();
                items = value;
                OnPropertyChanged("Items");
            }
        }

        private void ItemClick(int page)
        {
            string category = HymnDataAccess.Instance().GetCategory();
            string fileName = HymnDataAccess.Instance().GetFileName();
            DependencyService.Get<IAudio>().Close();
            DependencyService.Get<INativePage>().StartPage(category, fileName , page.ToString());
        }
    }
}
