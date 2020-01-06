using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using XamarinBible.Interface;
using XamarinBible.Model;

namespace XamarinBible.Database
{
    public class HymnDataAccess
    {
        private static HymnDataAccess instance = null;
        private SQLiteConnection database = null;
        private string table = "Hymn";

        private HymnDataAccess()
        {
            if (database == null)
            {
                database = DependencyService.Get<IBibleConnection>().DbConnection();
            }
        }
        public static HymnDataAccess Instance()
        {
            if (instance == null)
            {
                instance = new HymnDataAccess();
            }
            return instance;
        }

        public ObservableCollection<Hymn> GetList()
        {
            return new ObservableCollection<Hymn>( database.Query<Hymn>("select hymn_no as Number, hymn_title as Title, file_no as Page from "+ table) );
        }
        
        public void SetTableName(int position)
        {
            switch(position)
            {
               case 1:
                    table = "Hymn";
                    break;
                case 2:
                    table = "Hymn_gntc";
                    break;
                default:
                    table = "Hymn_child";
                    break;
            }
        }
        
        public string GetCategory()
        {
            switch (table)
            {
                case "Hymn":
                    return "hymn";

                case "Hymn_gntc":
                    return "gHymn";

                default:
                    return "kHymn";
            }
        }
        public string GetFileName()
        {
            switch (table)
            {
                case "Hymn":
                    return "hymn.pdf";

                case "Hymn_gntc":
                    return "gntc_hymn.pdf";

                default:
                    return "kid_hymn.pdf";
            }
        }

        public int GetPage(string number)
        {
            List<Hymn> hymn =  database.Query<Hymn>("select file_no as Page from " + table + " where hymn_no=" + number);

            if (hymn.Count > 0)
            {
                return database.Query<Hymn>("select file_no as Page from " + table + " where hymn_no=" + number)[0].Page;
            }
            else
            {
                return -1;
            }
        }


        public int GetHymnNo(int fileNo)
        {
            List<Hymn> hymn = database.Query<Hymn>("select hymn_no as Number from " + table + " where file_no=" + fileNo);

            if (hymn.Count == 1)
            {
                return hymn[0].Number;
            }
            else
            {
                return database.Query<Hymn>("select hymn_no as Number from " + table + " where file_no=" + (fileNo-1))[0].Number;
            }
        }



        // Album
        public ObservableCollection<Album> GetAlbumList()
        {
           return new ObservableCollection<Album>(  database.Query<Album>("select * from HymnAlbum") );
        }

    }
}
