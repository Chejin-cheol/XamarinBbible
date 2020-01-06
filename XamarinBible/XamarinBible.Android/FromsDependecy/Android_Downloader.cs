using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Net;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using XamarinBible.Droid.FromsDependecy;
using XamarinBible.Interface;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Net.Cache;

[assembly: Dependency(typeof(Android_Downloader))]
namespace XamarinBible.Droid.FromsDependecy
{
    public class Android_Downloader : IDownloader
    {
        public event EventHandler<DownloadEventArgs> OnFileDownloaded;
        public event EventHandler<ProgressEventArgs> OnProgress;
        public event EventHandler<ConnectivityChangedEventArgs> OnDownloadError;

        private Queue<string> _urls, _folders;
        private string url , pathToNewFile;
        private WebClient _webClient;

        public void DownloadFiles(Queue<string> urls, Queue<string> folders)
        {
            _urls = urls;
            _folders = folders;

            if (_webClient == null)
            {
                _webClient = new WebClient();
                _webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                _webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Progress);
            }
            Connectivity.ConnectivityChanged += NetworkStateChanged;
            DownloadFile();
        }

        private void DownloadFile()
        {


            url = _urls.Dequeue();
            string folder = _folders.Dequeue();
            string pathToNewFolder = Path.Combine(path1: System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), path2: folder);

            if (! Directory.Exists(pathToNewFolder))            //bible 폴더가 존재 x
            {
                Directory.CreateDirectory(pathToNewFolder);
            }
            
            try
            {
                pathToNewFile = Path.Combine(pathToNewFolder, Path.GetFileName(url));
                _webClient.DownloadFileAsync(new Uri(url), pathToNewFile);
            }
            catch (Exception ex)
            {
                if (OnFileDownloaded != null)
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(false , true));
            }
        }
        //completed
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (OnFileDownloaded != null)
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(false,true));
            }
            else
            {
                if (OnFileDownloaded != null && !_urls.Any())
                {
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(true));
                }
                else if (OnFileDownloaded != null)
                {
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(false));
                    DownloadFile();
                }
            }
        }
        
        // progress
        private async void Progress(object sender, DownloadProgressChangedEventArgs e)
        {
            double total = double.Parse( ((WebClient)sender).ResponseHeaders["Content-Length"] );
            var percent = (int)((e.BytesReceived / total) * 100);
            OnProgress.Invoke(this ,new ProgressEventArgs(percent));
        }

        private void NetworkStateChanged(Object sender ,ConnectivityChangedEventArgs e)
        {
            OnDownloadError.Invoke(this, e);

            if( ! _webClient.IsBusy && e.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                _webClient.DownloadFileAsync( new  Uri(url) , pathToNewFile);
            }
        }
    }
}