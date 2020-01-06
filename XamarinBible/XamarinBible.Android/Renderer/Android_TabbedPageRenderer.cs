using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Internal;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using XamarinBible.Droid.Renderer;
using XamarinBible.Page;

[assembly: ExportRenderer(typeof(MasterTabbedPage), typeof(Android_TabbedPageRenderer))]
namespace XamarinBible.Droid.Renderer
{
    public class Android_TabbedPageRenderer : TabbedPageRenderer
    {
        ViewPager _viewPager;
        BottomNavigationView _tabbar;

        public Android_TabbedPageRenderer(Context context) : base(context)
        {

        }

        


        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            try
            {
                base.OnElementChanged(e);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            if (Element == null) return;
            if (ViewGroup == null) return;

            var vg = (ViewGroup)GetChildAt(0);


            for (int i = 0; i < vg.ChildCount; i++)
            {
                var v = vg.GetChildAt(i);
               

                if(v is BottomNavigationView)
                {
                    BottomNavigationView _tabbar = v as BottomNavigationView;
                    

                    var topShadow = LayoutInflater.From(Context).Inflate(Resource.Layout.view_shadow, null);
                    var layoutParams =
                    new Android.Widget.RelativeLayout.LayoutParams(LayoutParams.MatchParent, 15);
                    layoutParams.AddRule(LayoutRules.Above, v.Id);
                    vg.AddView(topShadow, 2, layoutParams);
                    DisableShiftMode(_tabbar);

                }
            }
        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            
        }


        public void DisableShiftMode(BottomNavigationView v)
        {
            var menu = (BottomNavigationMenuView)((ViewGroup)v).GetChildAt(0);
            var shiftingMode = menu.Class.GetDeclaredField("mShiftingMode");
            shiftingMode.Accessible = true;
            shiftingMode.SetBoolean(menu, false);
            shiftingMode.Accessible = false;

            for (int i = 0; i < menu.ChildCount; i++)
            {
                BottomNavigationItemView item = (BottomNavigationItemView)menu.GetChildAt(i);
                item.SetShiftingMode(false);
                item.SetChecked(item.ItemData.IsChecked);


                //TextView
                var content = (BaselineLayout)item.GetChildAt(1);
                content.LayoutParameters = new Android.Widget.FrameLayout.LayoutParams(Android.Widget.FrameLayout.LayoutParams.MatchParent, Android.Widget.FrameLayout.LayoutParams.MatchParent);
                for (int j = 0; j < content.ChildCount; j++)
                {
                    var tv = (AppCompatTextView)content.GetChildAt(j);
                    tv.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                    tv.SetTextSize(Android.Util.ComplexUnitType.Px, tv.TextSize * 1.2f);
                    tv.TextAlignment = Android.Views.TextAlignment.Center;
                    tv.Gravity = GravityFlags.Center;
                    tv.Typeface = Android.Graphics.Typeface.DefaultBold;
                }
                item.RemoveView(item.GetChildAt(0));
                
            }

            menu.UpdateMenuView();
        }


        protected override void Dispose(bool disposing)
        {
            if(_tabbar!= null)
            {
                _tabbar.Dispose();
                _tabbar = null;
            }

            base.Dispose(disposing);
        }

    }

}