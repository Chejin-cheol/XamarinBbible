using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinBible.Behaviors;
using XamarinBible.Database;
using XamarinBible.DI;
using XamarinBible.DI.Service;
using XamarinBible.Interface;
using XamarinBible.Model;
using XamarinBible.ViewModel.Base;

namespace XamarinBible.ViewModel
{
    public class SermonViewModel : BaseViewModel ,IAudioViewModel
    {
        private ObservableCollection<Sermon> sermons = new ObservableCollection<Sermon>();
        private ISermonService _sermonService = null;
        private IAudio _audioService;
        public ICommand ClickCommand { get;  private set; }
        int book, chapter;
        private bool waitMode ,sermonMode , summaryMode;
        
        private string header = "";
        private string playerIcon = null;
        private double position=0, duration=1;
        
        private string title, summary;

        //sermon
        public ObservableCollection<Sermon> Sermons
        {
            get => sermons;
            set
            {
                sermons = value;
                OnPropertyChanged("Sermons");
            }
        }
        public string Header
        {
            get => header;
            set
            {
                header = value;
                OnPropertyChanged("Header");
            }
        }
        public double Position
        {
            get => position;
            set
            {
                position = value;
                OnPropertyChanged("Position");
            }
        }
        public double Duration
        {
            get => duration;
            set
            {
                    duration = value;
                    OnPropertyChanged("Duration");
            }
        }
        public string PlayerIcon
        {
            get => playerIcon;
            set
            {
                playerIcon = value;
                OnPropertyChanged("PlayerIcon");
            }
        }
        public IAudio Audio
        {
            get => _audioService;
            private set
            {
                _audioService = value;
                OnPropertyChanged("Audio");
            }
        }
        
        //Summary 
        public string Summary
        {
            get => summary;
            set
            {
                summary = value.Replace(System.Environment.NewLine, "").Replace(" ", "\u00A0");
                OnPropertyChanged("Summary");
            }
        }
        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }
        public Thickness TitlePadding
        {
            get => new Thickness(0, App.screenWidth / 20, 0, App.screenWidth / 20);
        }
        public Thickness SummaryPadding
        {
            get => new Thickness(App.screenWidth /20 , 0 ,App.screenWidth /20, 0 );
        }

        //page view mode
        public bool WaitMode
        {
            get => waitMode;
            set
            {
                waitMode = value;
                OnPropertyChanged("WaitMode");
            }
        }
        public bool SummaryMode
        {
            get => summaryMode;
            set
            {
                summaryMode = value;
                OnPropertyChanged("SummaryMode");
            }
        }
        public bool SermonMode
        {
            get => sermonMode;
            set
            {
                sermonMode = value;
                OnPropertyChanged("SermonMode");
            }
        }

        public SermonViewModel(ISermonService sermonService)
        {
            _sermonService = sermonService;
            _audioService = DependencyService.Get<IAudio>();
            _audioService.BindViewModel(this);
            ClickCommand = new Command<string>(OnClick);
            PlayerIcon = "ic_player_play.png";
        }

        //play icon
        public void PlayPause()
        {
            if (PlayerIcon == "ic_player_play.png")
            {
                PlayerIcon = "ic_player_pause.png";
            }
            else
            {
                PlayerIcon = "ic_player_play.png";
            }
        }

        public void OnClick(string param)
        {
            switch(param)
            {
                case "sermon":
                    Console.WriteLine("Sermon");
                    if(!SermonMode)
                    {
                        SermonMode = true;
                        SummaryMode = false;
                    }
                    break;
                case "summary":
                    Console.WriteLine("Summary");
                    if (!SummaryMode)
                    {
                        SermonMode = false;
                        SummaryMode = true;
                    }
                    break;

                case "player":
                    if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                    {
                        DependencyService.Get<IMessage>().LongAlert("인터넷 연결상태를 확인해주세요."); 
                        break;  
                    }
                    SetPlayList();
                    PlayPause();
                    if (!_audioService.Available())
                    {
                        Console.WriteLine("Available() " );
                        _audioService.Prepare(0);
                    }
                    else
                    {
                        _audioService.PlayPause();
                    }
                    break;
            }
        }

        public async void ChangeSermonList()
        {
            BookMark bookmark = BibleDataAccess.Instance().GetBookMark();
            if (book == bookmark.book && chapter == bookmark.chapter)
            {
                return;
            }
            try
            {
                WaitMode = !WaitMode;
                await Task.Run(() =>
                {
                    book = bookmark.book;
                    chapter = bookmark.chapter;

                    SummaryData summary = BibleDataAccess.Instance().GetSummary(book, chapter);
                    Title = summary.Title;
                    Summary = summary.Summary;

                    Header = BibleDataAccess.Instance().GetBibleName(book) + " " + chapter + "장";
                    Sermons = _sermonService.GetList(book, chapter);
                    
                    WaitMode = !WaitMode;
                });
            }
            catch (Exception e)
            {
                Console.WriteLine("Json Error : "+e);
            }
        }

        // Iviewmodel method
        public void SetPlayList()
        {
            if (_audioService.PlayList == null)
            {
                List<string> urls = new List<string>();
                foreach (Sermon sermon in sermons)
                {
                    string url = Constants.SermonAddress;
                    url += sermon.Date.Substring(0, 4) + "/";
                    url += sermon.Date + ".mp3";
                    urls.Add(url);
                }
                _audioService.PlayList = urls;
            }
        }

        public void Disappear()
        {
            _audioService.Close();
        }

        public void AudioChanged()
        {
        }
    }
}
