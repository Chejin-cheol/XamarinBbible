using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinBible.Helper;
using XamarinBible.iOS.Renderer;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(IOS_NavigationPageRenderer))]
namespace XamarinBible.iOS.Renderer
{
    class IOS_NavigationPageRenderer : NavigationRenderer
    {

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();

            if (NavigationBar != null)
            {
                NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
                NavigationBar.ShadowImage = new UIImage();
                NavigationBar.BackgroundColor = UIColor.White;
                NavigationBar.Layer.ShadowColor = UIKit.UIColor.Gray.CGColor;
                NavigationBar.Layer.ShadowOffset = new CoreGraphics.CGSize(0, 2);
                NavigationBar.Layer.ShadowOpacity = 0.5f;
                NavigationBar.Layer.ShadowRadius = 3;
            }
        }
    }
}