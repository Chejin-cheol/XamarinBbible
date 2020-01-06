using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBible.Helper
{
    public class LanguageHelper
    {
        public static string GetTopVersion(string[] item)
        {
            string top = item[0];

            for(int i =0;i< item.Length;i++)
            {
                if( GetOrder(top) > GetOrder(item[i]) )
                {
                    top = item[i];
                }
            }
            return top;
        }

        private static int GetOrder(string lang)
        {
            int result = 0;
            switch(lang)
            {
                case "'KOR'":
                    result = 1;
                    break;
                case "'NIV'":
                    result = 2;
                    break;
                case "'KJV'":
                    result = 3;
                    break;
                case "'JPN'":
                    result = 4;
                    break;
                case "'CHN'":
                    result = 5;
                    break;
            }
            return result;
        }
    }
}
