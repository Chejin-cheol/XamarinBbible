using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Artifex.MuPdfDemo;

namespace XamarinBible.Droid.Widget
{
    public class XFMuPDFReaderView : MuPDFReaderView
    {
        HymnActivity _activity;
        public XFMuPDFReaderView(Context context) : base(context)
        {
            _activity = (HymnActivity)context;
            
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);

            for(int i=0; i< ChildCount; i++)
            {
                MeasureView(GetChildAt(i));
            }
        }

        protected override void OnMoveToChild(int i)
        {
            base.OnMoveToChild(i);
            _activity.PageChanged(i);
        }

        public override bool OnSingleTapUp(MotionEvent e)
        {
            return true;
        }
    }
}