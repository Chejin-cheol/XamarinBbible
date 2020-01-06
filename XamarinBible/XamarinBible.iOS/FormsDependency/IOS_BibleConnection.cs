using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using SQLite;
using UIKit;
using Xamarin.Forms;
using XamarinBible.Interface;
using XamarinBible.iOS.FormsDependency;

[assembly: Dependency(typeof(IOS_BibleConnection))]
namespace XamarinBible.iOS.FormsDependency
{
    class IOS_BibleConnection : IBibleConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "bibles";
            var folderPath = Path.Combine(path1: System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments)
                                        , path2: "..", path3: "Library", path4: "database");
            var filePath = Path.Combine(folderPath, dbName);
            return new SQLiteConnection(filePath);
        }
    }
}