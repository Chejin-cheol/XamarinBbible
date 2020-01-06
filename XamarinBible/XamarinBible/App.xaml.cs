using Plugin.DeviceOrientation;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinBible.DI;
using XamarinBible.Helper;
using XamarinBible.Interface;
using XamarinBible.Login;
using XamarinBible.Page;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinBible
{
    public partial class App : Application
    {
        IPath platformPath = DependencyService.Get<IPath>();
        private static double width, height;
        private static Xamarin.Forms.Page masterPage;
        private DIContainer _container = null;
        
        public DIContainer DIContainer
        {
            get
            {
                if( !( Resources["di"] is null ) )
                {
                    return null;
                }

                if(_container == null)
                {
                    _container = Resources["di"] as DIContainer;
                }

                return _container;
            }
        }

        public static double screenWidth
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }
        public static double screenHeight
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        public static Xamarin.Forms.Page MasterPage
        {
            get
            {
                return masterPage;
            }
            set
            {
                masterPage = value;
            }
        }
        
        public App()
        {
            InitializeComponent();
            CrossDeviceOrientation.Current.LockOrientation(Plugin.DeviceOrientation.Abstractions.DeviceOrientations.Portrait);
            SetMainPage();
        }


        private void SetMainPage()
        {
                if (! Settings.FileSaved )
                {
                    MainPage = new DownloadPage();
                    return;
                }
                
               if (!string.IsNullOrEmpty(Settings.AccessToken))
               {
                /*
                   if (IdentityService.Certificate(Settings.AccessToken) != null)
                   {
                       App.MasterPage = new NavigationPage(new MasterTabbedPage());
                       App.Current.MainPage = App.MasterPage;
                   }
                */
                    App.MasterPage = new NavigationPage(new MasterTabbedPage());
                    App.Current.MainPage = App.MasterPage;
                }
               else
               {
                   MainPage = new LoginPage();
               }
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
