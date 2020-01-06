using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinBible.Control;
using XamarinBible.Droid.Renderer;

[assembly: ExportRenderer(typeof(BibleViewCell), typeof(Android_BibleViewCellRenderer))]
namespace XamarinBible.Droid.Renderer
{
    public class Android_BibleViewCellRenderer : ViewCellRenderer
    {
        private Android.Views.View _cellCore;

        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
        {
            _cellCore = base.GetCellCore(item, convertView, parent, context);
            return _cellCore;
        }

        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnCellPropertyChanged(sender, e);

            if(e.PropertyName == "IsSelected")
            {    
                _cellCore.SetBackgroundColor(Android.Graphics.Color.White);
            }
        }
    }
}