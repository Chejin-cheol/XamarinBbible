using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinBible.Database;
using XamarinBible.Interface;
using XamarinBible.Model;
using XamarinBible.ViewModel.Base;

namespace XamarinBible.ViewModel
{
    public class AlbumPlayViewModel : BaseViewModel , IAudioViewModel
    {

        public ICommand ClickCommand { get; set; }

        private IAudio _audioService;
        public IAudio Audio
        {
            get => _audioService;
            private set
            {
                _audioService = value;
                OnPropertyChanged("Audio");
            }
        }
        private ObservableCollection<Hymn> _playList;
        public ObservableCollection<Hymn> PlayList
        {
            get => _playList;
            set
            {
                _playList = value;
                OnPropertyChanged("PlayList");
            }
        }

        private string groupName;
        public string GroupName
        {
            get => groupName;
            set
            {
                groupName = value;
                OnPropertyChanged("GroupName");
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private double position = 0, duration = 1;
        private string positionText="00:00", durationText="00:00";
        public string PositionText
        {
            get => positionText;
            set
            {
                positionText = value;
                OnPropertyChanged("PositionText");
            }
        }
        public string DurationText
        {
            get => durationText;
            set
            {
                durationText = value;
                OnPropertyChanged("DurationText");
            }
        }

        // IAudioViewModel override
        public double Position
        {
            get => position;
            set
            {
                position = value;
                var min = (int)position / 60;
                var second = (int)position % 60;
                PositionText = string.Format("{0:D2}:{1:D2}", min, second);
                OnPropertyChanged("Position");
            }
        }
        public double Duration
        {
            get => duration;
            set
            {
                duration = value;
                var min = (int)duration / 60;
                var second = (int)duration % 60;
                DurationText = string.Format("{0:D2}:{1:D2}",min ,second);
                OnPropertyChanged("Duration");
            }
        }

        //
        private string playerIcon;
        public string PlayerIcon
        {
            get => playerIcon;
            set
            {
                playerIcon = value;
                OnPropertyChanged("PlayerIcon");
            }
        }

        public AlbumPlayViewModel()
        {
            _audioService = DependencyService.Get<IAudio>();
            _audioService.BindViewModel(this);
            ClickCommand = new Command<string>(OnClick);
            PlayerIcon = "ic_player_play.png";
        }

        // IAudioViewModel override
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

        public async void SetPlayList(string groupName ,int groupId)
        {
            await Task.Run(() =>
            {
                GroupName = groupName;
                if (groupId > 3)
                {

                }
                else
                {
                    HymnDataAccess.Instance().SetTableName(groupId);
                    var category = HymnDataAccess.Instance().GetCategory();
                    PlayList = HymnDataAccess.Instance().GetList();

                    Name = string.Format("{0}. {1}", PlayList[0].Number, PlayList[0].Title);
                    var playUrls = new List<string>();
                    foreach (Hymn item in PlayList)
                    {
                        playUrls.Add(string.Format(Constants.HymnAddress, category, item.Number));
                    }
                    Audio.PlayList = playUrls;
                    Audio.Prepare(0);
                    PlayPause();
                }
            });
        }
        // command

        public void OnClick(string param)
        {
            switch(param)
            {
                case "player":
                    PlayPause();
                    _audioService.PlayPause();
                    break;
            }
        }

        public void Distroy()
        {
            if(_audioService.Available())
            {
                _audioService.Close();
            }

            PlayList.Clear();
            PlayList = null;
        }

        public void AudioChanged()
        {
            var index = _audioService.Current;
            Name = string.Format("{0}. {1}", PlayList[index].Number, PlayList[index].Title);
        }
    }
}
