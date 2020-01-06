using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinBible.Droid.Renderer;
using XamarinBible.Control;
using System.ComponentModel;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using Android.Text;
using Android.Text.Style;
using Android.Graphics;
using XamarinBible.Droid.Widget;
using Android.Support.V4.Widget;
using Android.Util;

[assembly: ExportRenderer(typeof(HighlightLabel), typeof(Android_HighlightLabelRenderer))]
namespace XamarinBible.Droid.Renderer
{
    public class Android_HighlightLabelRenderer : LabelRenderer
    {
        Context _context;
        HighlightLabel _label;
        int textHeight;
        public Android_HighlightLabelRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            
            if(Control != null)
            {
                Control.SetLineSpacing(1,1.1f);
            }
            if(e.NewElement != null)
            {
                _label = (HighlightLabel)Element;
                _label.Text = _label.Text.Replace(" ", "\u00A0");
                textHeight = (int)_label.FontSize;

                if (_label.Dash)
                {
                    Control.PaintFlags |= Android.Graphics.PaintFlags.UnderlineText;
                    Control.PaintFlags |= Android.Graphics.PaintFlags.FakeBoldText;
                }
                else
                {
                    Control.PaintFlags &= ~Android.Graphics.PaintFlags.UnderlineText;
                    Control.PaintFlags &= ~Android.Graphics.PaintFlags.FakeBoldText;
                }
                
                if (_label.Highlight != null)
                {
                    
                    SpannableString text = new SpannableString(_label.Text);
                    text.SetSpan(new CustomLineSpan(Android.Graphics.Color.ParseColor(_label.Highlight), textHeight), 0, text.Length(), SpanTypes.ExclusiveExclusive);
                    
                    Control.SetText(text, TextView.BufferType.Normal);
                 
                }
                else
                {
                    Control.SetText(Control.Text, TextView.BufferType.Normal);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            
            if (e.PropertyName == "Dash")
            {

                if (_label.Dash)
                {
                    Control.PaintFlags |= Android.Graphics.PaintFlags.UnderlineText;
                    Control.PaintFlags |= Android.Graphics.PaintFlags.FakeBoldText;
                }
                else
                {
                    Control.PaintFlags &= ~Android.Graphics.PaintFlags.UnderlineText;
                    Control.PaintFlags &= ~Android.Graphics.PaintFlags.FakeBoldText;
                }
            }
            Console.WriteLine(e.PropertyName);
            if (e.PropertyName == "Highlight")
            {
                
                if(_label.Highlight != null)
                {
                    SpannableString text = new SpannableString(_label.Text);
                    text.SetSpan(new CustomLineSpan(Android.Graphics.Color.ParseColor(_label.Highlight), textHeight), 0, text.Length(), SpanTypes.ExclusiveExclusive);

                    Control.SetText(text, TextView.BufferType.Normal);
                }  
                else
                {
                    Control.SetText(Control.Text, TextView.BufferType.Normal);
                } 
            }
        }
    }
}