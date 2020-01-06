using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinBible.Control;
using XamarinBible.iOS.Renderer;
using XamarinBible.ViewModel;

[assembly: ExportRenderer(typeof(BibleListView), typeof(IOS_BibleListViewRenderer))]
namespace XamarinBible.iOS.Renderer
{
    public class IOS_BibleListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if(Control != null && Element != null)
            {
                Control.Delegate = new ListDelegate( (BibleViewModel)Element.BindingContext , new nfloat( App.screenHeight * 0.1f ) );
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "ChildFontSize")
            {
                Control.ReloadData();
            }
        }
    }


    public class ListDelegate : UITableViewDelegate
    {
        private nfloat lastOffsetY = 0f , _scope = 0f;
        private BibleViewModel _viewMdeol;

        public ListDelegate(BibleViewModel viewModel , nfloat scope)
        {
            _viewMdeol = viewModel;
            _scope = scope;
        }

        [Export("scrollViewWillBeginDragging:")]
        public override void DraggingStarted(UIScrollView scrollView)
        {
            Console.WriteLine("DraggingStarted");
            lastOffsetY = scrollView.ContentOffset.Y;
        }

        [Export("scrollViewDidScroll:")]
        public override void Scrolled(UIScrollView scrollView)
        {
            if(lastOffsetY - scrollView.ContentOffset.Y > 0)        //scroll up
            {
                if(Math.Abs(lastOffsetY - scrollView.ContentOffset.Y) > _scope)
                {
                    _viewMdeol.OptionMenuOpen = true;
                    lastOffsetY = scrollView.ContentOffset.Y;
                }
            }
            else
            {
                if (Math.Abs(lastOffsetY - scrollView.ContentOffset.Y) > _scope)       // scroll down
                {
                    _viewMdeol.OptionMenuOpen = false;
                    lastOffsetY = scrollView.ContentOffset.Y;
                }
            }
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _viewMdeol = null;
        }
    }
}