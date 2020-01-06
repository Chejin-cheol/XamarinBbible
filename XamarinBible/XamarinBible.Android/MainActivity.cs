using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.CurrentActivity;
using Android.Content.Res;
using Plugin.DeviceOrientation;
using System.Runtime.Remoting.Contexts;
using Xamarin.Forms;
using CarouselView.FormsPlugin.Android;
//using CarouselView.FormsPlugin.Android;

namespace XamarinBible.Droid
{
    [Activity(Label = "XamarinBible", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static Android.Content.Context _context;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            _context = this;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            CrossCurrentActivity.Current.Activity = this;

           
            //global::Xamarin.Forms.Forms.SetFlags("Shell_Experimental", "CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CarouselViewRenderer.Init();
            LoadApplication(new App());



            #region For screen Height & Width

            var pixels = Resources.DisplayMetrics.WidthPixels;
            var scale = Resources.DisplayMetrics.Density;

            var dps = (double)((pixels - 0.5f) / scale);

            var ScreenWidth = (int)dps;

            App.screenWidth = ScreenWidth;

            pixels = Resources.DisplayMetrics.HeightPixels;
            dps = (double)((pixels - 0.5f) / scale);

            var ScreenHeight = (int)dps;
            App.screenHeight = ScreenHeight;

            #endregion
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            DeviceOrientationImplementation.NotifyOrientationChange(newConfig.Orientation);
        }


        public override Resources Resources
        {
            get
            {
                    Resources res = base.Resources;
                    Configuration config = new Configuration();
                    config.SetToDefaults();
                    res.UpdateConfiguration(config, res.DisplayMetrics);
                return res;
            }
        }
    }
}