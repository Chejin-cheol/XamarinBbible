using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using XamarinBible.Droid.FromsDependecy;
using XamarinBible.Interface;

[assembly: Dependency(typeof(Android_Path))]
namespace XamarinBible.Droid.FromsDependecy
{
    public class Android_Path : IPath
    {
        public string getLocalPath(string forderName, string fileName)
        {
            return Path.Combine(path1: System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), path2: forderName, path3: fileName);
        }

        public string getLocalPath(string Directory)
        {
            return Path.Combine(path1: System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), path2: Directory);
        }
    }
}