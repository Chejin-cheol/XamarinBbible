using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinBible.Helper;
using XamarinBible.Interface;
using XamarinBible.Login;
using XamarinBible.Page;

namespace XamarinBible
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DownloadPage : ContentPage
    {
        IPath platformPath = DependencyService.Get<IPath>();
        IDownloader downloader = DependencyService.Get<IDownloader>();
        private int currentCount, allCount = 0;


        public DownloadPage()
        {
            InitializeComponent();
            downloader.OnFileDownloaded += OnFileDownloaded;
            downloader.OnProgress += OnProgress;
            downloader.OnDownloadError += OnDownloadedError;
            InitResource();
        }

        public void InitResource()
        {
            Queue<string> items = new Queue<string>();
            Queue<string> folders = new Queue<string>();

            items.Enqueue(Constants.SourceUrl + "/sqlite/bibles");
            items.Enqueue(Constants.SourceUrl + "/sqlite/gntc_hymn.pdf");
            items.Enqueue(Constants.SourceUrl + "/sqlite/hymn.pdf");
            items.Enqueue(Constants.SourceUrl + "/sqlite/kid_hymn.pdf");

            folders.Enqueue("database");
            folders.Enqueue("gHymn");
            folders.Enqueue("hymn");
            folders.Enqueue("kHymn");

            allCount = folders.Count;
            currentCount++;
            count.Text = string.Format("({0}/{1})", currentCount, allCount);
            downloader.DownloadFiles(items, folders);
        }

        public void OnProgress(object sender, ProgressEventArgs e)
        {
            message.Text = e.Percent + "% 다운로드";
        }

        public void OnFileDownloaded(Object sender, DownloadEventArgs e)
        {
            if (! e.IsFailed)
            {
                if(e.FileSaved)
                {
                    Settings.FileSaved = true;
                    App.Current.MainPage = new LoginPage();
                }
                else
                {
                    currentCount++;
                    count.Text = string.Format("({0}/{1})", currentCount, allCount);
                }
            }
            else
            {
                message.Text = "네트워크 확인 중...";
                count.Text = "";
            }
        }

        public void OnDownloadedError(Object sender, ConnectivityChangedEventArgs e)
        {
            if(e.NetworkAccess == NetworkAccess.Local)
            {
                message.Text = "네트워크 확인 중...";
                count.Text = "";
            }
            else if(e.NetworkAccess == NetworkAccess.Internet)
            {
                message.Text = "";
                count.Text = string.Format("({0}/{1})", currentCount, allCount);
            }
        } 
    }
}