using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Settings;
using Plugin.Settings.Abstractions;


namespace XamarinBible.Helper
{
    public static class Settings
    {

        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public static string AccessToken
        {
            get
            {
                //         return AppSettings.GetValueOrDefault<string>("AccessToken", "");
                return AppSettings.GetValueOrDefault("AccessToken", "");
            }
            set
            {
                //       AppSettings.AddOrUpdateValue<string>("AccessToken", value);
                AppSettings.AddOrUpdateValue("AccessToken", value);
            }
        }

        public static DateTime AccessTokenExpirationDate
        {
            get
            {
                return AppSettings.GetValueOrDefault("AccessTokenExpirationDate", DateTime.UtcNow);
            }
            set
            {
                AppSettings.AddOrUpdateValue("AccessTokenExpirationDate", value);
            }
        }

        public static bool FileSaved
        {
            get
            {
                //         return AppSettings.GetValueOrDefault<string>("AccessToken", "");
                return AppSettings.GetValueOrDefault("FileSaved", false);
            }
            set
            {
                //       AppSettings.AddOrUpdateValue<string>("AccessToken", value);
                AppSettings.AddOrUpdateValue("FileSaved", value);
            }
        }

        // Bible langauge
        public static string Languages
        {
            get
            {
                return AppSettings.GetValueOrDefault("Languages", "'KOR'");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Languages",value);
            }
        }

        public static double FontSize
        {
            get
            {
                return AppSettings.GetValueOrDefault("FontSize", 0.0);
            }
            set
            {
                AppSettings.AddOrUpdateValue("FontSize", value);
            }
        }

        public static int CellSpace
        {
            get
            {
                return AppSettings.GetValueOrDefault("CellSpace", 0);
            }
            set
            {
                AppSettings.AddOrUpdateValue("CellSpace", value);
            }
        }


        public static bool LightMode
        {
            get
            {
                return AppSettings.GetValueOrDefault("LightMode", true);
            }
            set
            {
                AppSettings.AddOrUpdateValue("LightMode", value);
            }
        }

        
    }
}
