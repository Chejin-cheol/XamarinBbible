using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace XamarinBible.Droid.Widget
{
    public class CustomLineSpan : Java.Lang.Object, ILineBackgroundSpan 
    {
        private Android.Graphics.Color backgroundColor;
        private int padding;

        public CustomLineSpan(Android.Graphics.Color color ,int padding)
        {
            
            backgroundColor = color;
            this.padding = padding;
            
        }

        public void DrawBackground(Canvas c, Paint p, int left, int right, int top, int baseline, int bottom, ICharSequence text, int start, int end, int lnum)
        {
            int textWidth = Java.Lang.Math.Round(p.MeasureText(text, start, end));
            Android.Graphics.Color paintColor= p.Color;

            RectF rect = new RectF();
            //   Rect rect = new Rect();
            rect.Set(left,top +padding/2 ,left + textWidth,  bottom  -padding/2);
            p.Color = backgroundColor;
            //           c.DrawRect(rect,p);
            c.DrawRoundRect(rect ,15,15, p);
            p.Color = paintColor;
        }
    }
}