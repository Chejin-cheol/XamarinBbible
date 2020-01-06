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
    public class DashedUnderlineSpan : Java.Lang.Object, ILineBackgroundSpan, ILineHeightSpan
    {
        private Paint paint;
        private TextView textView;
        private float offsetY;
        private float spacingExtra;

        public void ChooseHeight(ICharSequence text, int start, int end, int spanstartv, int v, Paint.FontMetricsInt fm)
        {
            throw new NotImplementedException();
        }

        /*
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        */

        public void DrawBackground(Canvas c, Paint p, int left, int right, int top, int baseline, int bottom, ICharSequence text, int start, int end, int lnum)
        {
            throw new NotImplementedException();
        }
    }
}