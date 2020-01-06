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
using XamarinBible.Control;
using XamarinBible.Droid.Renderer;
using XamarinBible.ViewModel;
using static Android.Views.View;
using static Android.Widget.AbsListView;

[assembly: ExportRenderer(typeof(BibleListView), typeof(Android_BibleListViewRenderer))]
namespace XamarinBible.Droid.Renderer
{
    class Android_BibleListViewRenderer : ListViewRenderer
    {
        Context  _context;
        public Android_BibleListViewRenderer(Context context) : base(context)
        {
            _context = context;
        }


        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);


            if (Element == null && Control == null)
                return;

            int scope = (int)(_context.Resources.DisplayMetrics.HeightPixels * 0.1);
            Control.SetOnScrollListener(new DirctionCheckListener((BibleViewModel)Element.BindingContext ,scope));
            
        }

    }

    public class DirctionCheckListener : Java.Lang.Object, IOnScrollListener
    {
        private int previousDistanceFromFirstCellToTop ;
        private bool isFling = false;
        private int deltaHeight = 0;
        private double scope = 0;
        public BibleViewModel _viewModel;
        public DirctionCheckListener(BibleViewModel viewModel , int scope)
        {
            _viewModel = viewModel;
            this.scope = scope;
        }

        public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
        {
            if(isFling)
            {
                return;
            }

            Android.Views.View firstCell = view.GetChildAt(0);

            if (firstCell == null) { return; }
            int distanceFromFirstCellToTop = view.FirstVisiblePosition * firstCell.Height - firstCell.Top;
            
            // ***
            if(previousDistanceFromFirstCellToTop !=0 )
            {
                deltaHeight += Math.Abs(distanceFromFirstCellToTop - previousDistanceFromFirstCellToTop);
            }

            if (deltaHeight >= scope)
            {
                if (distanceFromFirstCellToTop < previousDistanceFromFirstCellToTop)
                {
                    //Scroll Up
                    if (!_viewModel.OptionMenuOpen)
                    {
                        _viewModel.OptionMenuOpen = true;
                    }
                    else
                    {
                        deltaHeight = 0;
                    }
                }
                else if (distanceFromFirstCellToTop > previousDistanceFromFirstCellToTop)
                {

                    //Scroll Down
                    if (_viewModel.OptionMenuOpen)
                    {
                        _viewModel.OptionMenuOpen = false;
                    }
                    else
                    {
                        deltaHeight = 0;
                    }
                }
            }
            previousDistanceFromFirstCellToTop = distanceFromFirstCellToTop;
        }

        public void OnScrollStateChanged(AbsListView view, [GeneratedEnum] ScrollState scrollState)
        {
            if(scrollState == ScrollState.Fling)
            {
                isFling = true;
            }

            if (scrollState == ScrollState.Idle)
            {
                isFling = false;
                deltaHeight = 0;
            }
        }

    }


}