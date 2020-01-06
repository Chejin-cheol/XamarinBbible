using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using XamarinBible.Interface;
using XamarinBible.iOS.FormsDependency;

[assembly: Dependency(typeof(IOS_Message))]
namespace XamarinBible.iOS.FormsDependency
{
    public class IOS_Message : IMessage
    {
        public void LongAlert(string message)
        {
            throw new NotImplementedException();
        }

        public void ShortAlert(string message)
        {
            throw new NotImplementedException();
        }
    }
}