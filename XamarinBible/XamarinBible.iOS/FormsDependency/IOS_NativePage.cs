using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using XamarinBible.Interface;
using XamarinBible.iOS.FormsDependency;
using XamarinBible.iOS.ViewController;

[assembly: Dependency(typeof(IOS_NativePage))]
namespace XamarinBible.iOS.FormsDependency
{
    public class IOS_NativePage : INativePage
    {
        public void StartPage(string category, string fileName, string page)
        {
            var rootController = ((AppDelegate)(UIApplication.SharedApplication.Delegate)).Window.RootViewController.ChildViewControllers[0].ChildViewControllers[0].ChildViewControllers[0];
            var navcontroller = rootController as UINavigationController;
            if (navcontroller != null)
                rootController = navcontroller.VisibleViewController;
            rootController.PresentViewController(new PDFViewController(category, fileName , page), true, null);
        }
    }
}