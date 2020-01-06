using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBible.ViewModel
{
    public class DownloadViewModel : BaseViewModel
    {
        private string downloadName;
        public string DownloadName
        {
            get => downloadName+ " 다운로드중...";
            set
            {
                downloadName = value;
                OnPropertyChanged("DownloadName");
            }
        }

        private int downloadProgress;
        public int DownloadProgress
        {
            get => downloadProgress;
            set
            {
                downloadProgress = value;
                OnPropertyChanged("DownloadProgress");
            }
        }

        private int downloadSize;
        public int DownloadSize
        {
            get => downloadSize;
            set
            {
                downloadSize = value;
                OnPropertyChanged("DownloadSize");
            }
        }
    }
}
