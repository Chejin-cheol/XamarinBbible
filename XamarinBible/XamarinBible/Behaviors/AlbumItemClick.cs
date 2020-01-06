using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinBible.ViewModel;

namespace XamarinBible.Behaviors
{
    public class AlbumItemClick : Behavior<ListView>
    {
        protected override void OnAttachedTo(ListView bindable)
        {
            bindable.ItemSelected += ItemSelected;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            bindable.ItemSelected -= ItemSelected;
            base.OnDetachingFrom(bindable);
        }

        private async void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var vm = (AlbumPlayViewModel)((ListView)sender).BindingContext;
            if (vm.Audio.IsPlaying())
            {
                vm.Audio.Current = e.SelectedItemIndex;
                vm.Audio.Stop();
            }
 
        }
    }
}
