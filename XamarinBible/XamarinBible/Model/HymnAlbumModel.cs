using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinBible.Interface;
using XamarinBible.Page;
using XamarinBible.ViewModel;

namespace XamarinBible.Model
{
    public class HymnAlbumModel : HymnModel
    {

        public HymnAlbumModel(HymnViewModel vm) : base(vm)
        {
            OuterViewModel = vm;
            ItemClickCommand = new Command<int>(ItemClick);
        }

        private ObservableCollection<Album> items = new ObservableCollection<Album>();
        public ObservableCollection<Album> Items
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

        private async void ItemClick(int id)
        {
            var item = Items.Single(i => i.ID == id);
            await App.Current.MainPage.Navigation.PushAsync(new AlbumPlayPage(item.AlbumName ,id));
        }
    }
}
