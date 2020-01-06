using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xamarin.Essentials;

namespace XamarinBible.Interface
{
    public interface IDownloader
    {
        void DownloadFiles(Queue<string> url, Queue<string> folder);
        event EventHandler<DownloadEventArgs> OnFileDownloaded;
        event EventHandler<ProgressEventArgs> OnProgress;
        event EventHandler<ConnectivityChangedEventArgs> OnDownloadError;
    }
    public class DownloadEventArgs : EventArgs
    {
        public bool FileSaved = false;
        public bool IsFailed = false;
        public DownloadEventArgs(bool fileSaved)
        {
            FileSaved = fileSaved;
        }
        public DownloadEventArgs(bool fileSaved , bool isFailed)
        {
            FileSaved = fileSaved;
            IsFailed = isFailed;
        }
    }
    public class ProgressEventArgs : EventArgs
    {
        public int Percent = 0;
        public ProgressEventArgs(int percent)
        {
            Percent = percent;
        }
    }
}
