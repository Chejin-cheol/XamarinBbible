using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinBible.Interface;
using XamarinBible.Model;
using XamarinBible.ViewModel;

namespace XamarinBible.Behaviors
{
    public class SermonListClick : Behavior<ListView>
    {
        public static readonly BindableProperty AudioProperty = BindableProperty.Create(nameof(Audio), typeof(IAudio), typeof(SermonListClick), null);
        public IAudio Audio
        {
            get { return (IAudio)GetValue(AudioProperty); }
            set { SetValue(AudioProperty, value); }
        }

        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.ItemSelected += ItemClick;
        }
        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.ItemSelected -= ItemClick;
        }

        public async void ItemClick(Object sender , SelectedItemChangedEventArgs e)
        {
            ((SermonViewModel)((View)sender).BindingContext).PlayPause();
            if( Audio.IsPlaying() )
            {
                Audio.Stop();
            }

            await Task.Run(() =>
            {
                var sermon = (Sermon)e.SelectedItem;
                var item = new List<string>();
                
                item.Add(Constants.SermonAddress + sermon.Date.Substring(0, 4) + "/" + sermon.Date + ".mp3");
                Audio.PlayList = item;
                Audio.Prepare(0);
            });
        }
    }
}
