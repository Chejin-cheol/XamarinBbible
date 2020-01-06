using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using XamarinBible.Helper;
using XamarinBible.Interface;
using XamarinBible.Model;

namespace XamarinBible.Database
{
    // singletone claass


    public class BibleDataAccess
    {
        private static BibleDataAccess instance = null;
        private SQLiteConnection database = null;

        public enum Mode
        {
            Init,
            Prev,
            Next
        }
        private string top = "'KOR'";
        private string ORDER_CASE = " case version when 'KOR' then 1 " +
                        " when 'NIV' then 2" +
                        " when 'KJV' then 3" +
                        " when 'JPN' then 4" +
                        " when 'CHN' then 5" +
                        " else 9 end ver ";
        public string TOP_CASE
        {
            set
            {
                top = value;
            }
            get
            {
                return " case when not version = " + top + " then '' else paragraph end para ";
            }
        }
    
        //constructor
        public BibleDataAccess()
        {
            if (database == null)
            {
                database = DependencyService.Get<IBibleConnection>().DbConnection();
            }
        }
        public static BibleDataAccess Instance()
        {
            if (instance == null)
            {
                instance = new BibleDataAccess();
            }
            return instance;
        }

        //bible paging 

        public List<Bible> GetBible(Mode mode)
        {
            BookMark bookmark = GetBookMark();
            int book = bookmark.book;
            int chapter = bookmark.chapter;

            switch (mode)
            {
                case Mode.Init:
                    return database.Query<Bible>("select "+TOP_CASE+" ,"+ORDER_CASE+ ", id, version,book,chapter,paragraph,replace( sentence ,'\n','') as sentence ,mark , highlight " +
                                                "from bible where book=" + book + " and chapter=" + chapter + " and " +
                                                "version IN ("+Settings.Languages+") order by book,chapter,paragraph, ver");
                case Mode.Prev:

                    if(book==1 && chapter ==1)
                    {
                        return null;
                    }
                    else
                    {
                        return GetPrevContent(book, --chapter);
                    }
               

                case Mode.Next:

                    if(book == 66 && chapter == 22)
                    {
                        return null;
                    }
                    else
                    {
                        return GetNextContent(book, ++chapter);
                    }

                default:
                    return null;

            }
        }

        public List<Bible> GetPrevContent(int book, int chapter)
        {
            List<Bible> bible = SearchBibile(book, chapter);

            if (bible.Count != 0)
            {
                SetBookMark(book, chapter);
                return bible;
            }
            else
            {
                --book;

                bible = database.Query<Bible>("select " + TOP_CASE + " ," + ORDER_CASE + ", id, version,book,chapter,paragraph,replace( sentence ,'\n','') as sentence ,mark , highlight " +
                    "                         from bible where book=" + book + " and chapter = (select max(chapter) from bible where book=" + book + ") and " +
                                              "version IN (" + Settings.Languages + ") order by book,chapter,paragraph, ver");
                SetBookMark(book, bible[0].chapter);
                return bible;
            }
        }


        public List<Bible> GetNextContent(int book, int chapter)
        {
            List<Bible> bible = SearchBibile(book, chapter);
            if (bible.Count != 0)
            {
                SetBookMark(book, chapter);
                return bible;
            }
            else
            {

                SetBookMark(++book, 1);
                bible = SearchBibile(book, 1);
                return bible;
            }
        }

        // search bible
        public List<Bible> SearchBibile(int book, int chapter)
        {
            SetBookMark(book, chapter);
            return database.Query<Bible>("select " + TOP_CASE + " ," + ORDER_CASE + ", id, version,book,chapter,paragraph,replace( sentence ,'\n','') as sentence ,mark , highlight " +
                                                "from bible where book=" + book + " and chapter=" + chapter + " and " +
                                                "version IN (" + Settings.Languages + ") order by book,chapter,paragraph, ver");
        }

        // bookmark
        public void SetBookMark(int book, int chapter)
        {
            database.Query<BookMark>("update bookmark set book=" + book + ", chapter=" + chapter);
        }

        public BookMark GetBookMark()
        {
            List<BookMark> bookmark = database.Query<BookMark>("select * from bookmark");

            if (bookmark.Count != 0)
            {
                return bookmark[0];
            }
            else
            {
                database.Query<BookMark>("insert into bookmark(book,chapter) values(1,1)");
                return database.Query<BookMark>("select * from bookmark where book=1 and chapter=1")[0];
            }
        }


        // color option

        public List<Colors> GetColors()
        {
            return database.Query<Colors>("select * from ColorOption order by id desc");
        }
        public void MarkHighlight(int id , string color )
        {
            Console.WriteLine("UPDATE bible SET mark=" + true + " , highlight=" + color + " where id=" + id);
            database.Query<BookMark>("UPDATE bible SET mark=1 , highlight='"+color +"'  where id="+id);
        }



        // get bible meta data

        public string GetBibleName(int book)
        {
            List<SearchResult> list;

            list = database.Query<SearchResult>("select name as Value from bible_label where id="+book);

            if (list.Count > 0)
            {
                return list[0].Value;
            }
            else
            {
                return null;
            }
        }

        public ObservableCollection<SearchResult> GetBibleNames()
        {
            ObservableCollection<SearchResult> list;

            list = new ObservableCollection<SearchResult>(database.Query<SearchResult>("select name as Value from bible_label"));

            return list;
        }

        public int[] GetIndexCount(int book,int chapter = 1)
        {
            int[] index = new int[2];

            index[0] = database.Query<IndexCount>("select count(chapter) as Count from (select distinct chapter from bible where book=" + book + ")")[0].Count;

            index[1] = database.Query<IndexCount>("select distinct count(paragraph) as Count from " +
                                               "(select distinct paragraph from bible where book="+book+" and chapter="+chapter+")")[0].Count;

            return index;
        }
        // Summary data
        public SummaryData GetSummary(int book , int chapter)
        {
            BookMark bookMark = GetBookMark();
            return database.Query<SummaryData>("select Summary , Title from Home_Service where Book="+ book +" and ( SChapter="+chapter +" or EChapter="+chapter+" )")[0];
        }
    }

    class IndexCount
    {
        public int Count { get; set; }
    }
}
