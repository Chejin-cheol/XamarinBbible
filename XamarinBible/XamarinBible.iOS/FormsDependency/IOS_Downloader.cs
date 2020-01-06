using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinBible.Interface;
using XamarinBible.iOS.FormsDependency;

[assembly: Dependency(typeof(IOS_Downloader))]
namespace XamarinBible.iOS.FormsDependency
{
    class IOS_Downloader : IDownloader
    {  
        public event EventHandler<DownloadEventArgs> OnFileDownloaded;
        public event EventHandler<ProgressEventArgs> OnProgress;
        public event EventHandler<ConnectivityChangedEventArgs> OnDownloadError; //no 

        private Queue<string> _urls, _folders;


        public void DownloadFiles(Queue<string> urls, Queue<string> folders)
        {
            _urls = urls;
            _folders = folders;
            DownloadFile();
        }

        public void DownloadFile()
        {
            string url = _urls.Dequeue();
            string folder = _folders.Dequeue();

            string pathToNewFolder = Path.Combine(path1: System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), path2: "..", path3: "Library", path4: folder);

            if (Directory.Exists(pathToNewFolder))            //bible 폴더가 존재하는 경우
            {
                OnFileDownloaded.Invoke(this, new DownloadEventArgs(false));    // 이미 존재하므로 file download 불가능    
                return;
            }
            Directory.CreateDirectory(pathToNewFolder);

            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                string pathToNewFile = Path.Combine(pathToNewFolder, Path.GetFileName(url));
                webClient.DownloadFileAsync(new Uri(url), pathToNewFile);
            }
            catch (Exception ex)
            {
                if (OnFileDownloaded != null)
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(false));
            }

        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (OnFileDownloaded != null)
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(false));
            }
            else
            {
                if (OnFileDownloaded != null && !_urls.Any())
                {
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(true));
                }
                else if (OnFileDownloaded != null)
                {
                    DownloadFile();
                }

            }
        }
    }
}