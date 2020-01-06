using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using XamarinBible.Database;
using XamarinBible.DI.Service;
using XamarinBible.Model;

namespace XamarinBible.Behaviors
{
    public class HymnSearchTextChange : Behavior<SearchBar>
    {
        public ObservableCollection<Hymn> temp = new ObservableCollection<Hymn>();

        public static readonly BindableProperty MatcherProperty = BindableProperty.Create(nameof(Matcher), typeof(IKoreanMatchService), typeof(HymnSearchTextChange), null);
        public IKoreanMatchService Matcher
        {
            get { return (IKoreanMatchService)GetValue(MatcherProperty); }
            set { SetValue(MatcherProperty, value); }
        }

        protected override void OnAttachedTo(SearchBar bindable)
        {
            bindable.TextChanged += OnSearch;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(SearchBar bindable)
        {
            bindable.TextChanged -= OnSearch;
            temp.Clear();
            temp = null;
            base.OnDetachingFrom(bindable);
        }

        private void OnSearch(Object sender, TextChangedEventArgs e)
        {
            var list = HymnDataAccess.Instance().GetList();
            
            if ( string.IsNullOrEmpty( e.NewTextValue ))
            {
                ((HymnListModel)BindingContext).Items = list;
                return;
            }

            temp.Clear();
            temp = null;
            temp = new ObservableCollection<Hymn>();

            for (int i = 0; i < list.Count; i++)
            {
                if (Matcher.Match(list[i].Title, e.NewTextValue) || list[i].Number.ToString().Contains(e.NewTextValue) )
                {
                    temp.Add(new Hymn() { Number = list[i].Number, Title = list[i].Title, Page = list[i].Page });
                }
            }
            ((HymnListModel)BindingContext).Items = temp;
        }
    }
}
