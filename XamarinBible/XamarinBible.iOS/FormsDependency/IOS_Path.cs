using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using XamarinBible.Interface;
using XamarinBible.iOS.FormsDependency;

[assembly: Dependency(typeof(IOS_Path))]

namespace XamarinBible.iOS.FormsDependency
{
    public class IOS_Path : IPath
    {
        public string getLocalPath(string forderName, string fileName)
        {
            string folderDir = Path.Combine(path1: System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), path2: "..", path3: "Library", path4: forderName);
            return Path.Combine(folderDir, fileName);
        }

        public string getLocalPath(string Directory)
        {
            return null;
        }
    }
}