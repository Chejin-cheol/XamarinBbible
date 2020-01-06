using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using ObjCRuntime;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinBible.iOS.Renderer;
using XamarinBible.Page;

[assembly: ExportRenderer(typeof(MasterTabbedPage), typeof(IOS_TabbedPageRenderer))]
namespace XamarinBible.iOS.Renderer
{
    class IOS_TabbedPageRenderer : TabbedRenderer
    {
        public bool isDark = false;

        public override void ViewWillLayoutSubviews()
        {
            base.ViewWillLayoutSubviews();

            if (TabBar != null)
            {
                TabBar.ShadowImage = new UIImage();
                TabBar.BackgroundImage = new UIImage();
                TabBar.BackgroundColor = UIColor.White;
                TabBar.Translucent = false;

                TabBar.Layer.ShadowColor = UIColor.Black.CGColor;
                TabBar.Layer.ShadowOpacity = 0.5f;
                TabBar.Layer.ShadowOffset = new CoreGraphics.CGSize(0, 2);
                TabBar.Layer.ShadowRadius = 4;
            }
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();

            
            foreach (var item in TabBar.Items)
            {
                UITextAttributes normal = new UITextAttributes()
                {
                    Font = UIFont.SystemFontOfSize((float)Device.GetNamedSize(NamedSize.Medium, typeof(Label)))
                };
                item.SetTitleTextAttributes(normal, UIControlState.Normal);

                UITextAttributes selected = new UITextAttributes()
                {
                    Font = UIFont.SystemFontOfSize((float)Device.GetNamedSize(NamedSize.Medium, typeof(Label)) * 1.2f)
                };
                item.SetTitleTextAttributes(selected, UIControlState.Selected);

                item.TitlePositionAdjustment = new UIOffset(new nfloat(0.0) , new nfloat(-TabBar.Subviews[0].Frame.Height / 6));
            }
        }




        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != e.OldElement)
            {
                if (e.OldElement != null)
                {
                    e.OldElement.PropertyChanged -= OnElementPropertyChanged;
                }
                if (e.NewElement != null)
                {
                    e.NewElement.PropertyChanged += OnElementPropertyChanged;
                }
            }
        }


        void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "BarBackgroundColor" || e.PropertyName == "BarTextColor" )
            {
                foreach (var item in TabBar.Items)
                {
                    UITextAttributes normal = new UITextAttributes()
                    {
                        Font = UIFont.SystemFontOfSize((float)Device.GetNamedSize(NamedSize.Medium, typeof(Label)))
                    };
                    item.SetTitleTextAttributes(normal, UIControlState.Normal);

                    UITextAttributes selected = new UITextAttributes()
                    {
                        Font = UIFont.SystemFontOfSize((float)Device.GetNamedSize(NamedSize.Medium, typeof(Label)) * 1.2f)
                    };
                    item.SetTitleTextAttributes(selected, UIControlState.Selected);

                    item.TitlePositionAdjustment = new UIOffset(new nfloat(0.0), new nfloat(-TabBar.Subviews[0].Frame.Height / 6));
                }
            }
        }
    }
}