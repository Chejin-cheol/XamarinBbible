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
using SQLite;
using Xamarin.Forms;
using XamarinBible.Droid.FromsDependecy;
using XamarinBible.Interface;

[assembly: Dependency(typeof(Android_BibleConnection))]
namespace XamarinBible.Droid.FromsDependecy
{
    class Android_BibleConnection : IBibleConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "bibles";
            var folderPath = Path.Combine(System.Environment.
              GetFolderPath(System.Environment.
              SpecialFolder.MyDocuments), "database");
            var filePath = Path.Combine(folderPath, dbName);

            return new SQLiteConnection(filePath);
        }
    }
}