using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinBible.Model;

namespace XamarinBible.Control
{
    public class BibleListView : ListView
    {
        public BibleListView()
        {
        }


        public static readonly BindableProperty ScrollPositionProperty = BindableProperty.Create(nameof(ScrollPosition), typeof(int), typeof(BibleSlideUpMenu), 0, propertyChanged: StatusChaged);
        public int ScrollPosition
        {
            get { return (int)GetValue(ScrollPositionProperty); }
            set { SetValue(ScrollPositionProperty, value); }
        }

        public static readonly BindableProperty ChildFontSizeProperty = BindableProperty.Create(nameof(ChildFontSize), typeof(double), typeof(BibleListView), 0.0, propertyChanged: FontSizeChanged);
        public double ChildFontSize
        {
            get { return (double)GetValue(ScrollPositionProperty); }
            set { SetValue(ScrollPositionProperty, value); }
        }

        private static void StatusChaged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null)
            {
                var listview = ((BibleListView)bindable);
                ObservableCollection<Bible> items = (ObservableCollection<Bible>)listview.ItemsSource;
                int index = (int)newValue;
                listview.ScrollTo( items[index] ,ScrollToPosition.Start , false );
            }
        }

        private static void FontSizeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    //var listview = ((BibleListView)bindable);
                    //listview.ItemsSource = listview.ItemsSource;
                }
            }
            }
        }
    }
