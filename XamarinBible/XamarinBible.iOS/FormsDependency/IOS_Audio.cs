using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AVFoundation;
using Foundation;
using UIKit;
using Xamarin.Forms;
using XamarinBible.Interface;
using XamarinBible.iOS.FormsDependency;
using XamarinBible.ViewModel.Base;

[assembly: Dependency(typeof(IOS_Audio))]
namespace XamarinBible.iOS.FormsDependency
{
    public class IOS_Audio : IAudio
    {
        IAudioViewModel _viewModel;
        AVAudioPlayer _player;

        Thread audioThread;

        private List<string> _playList;
        public List<string> PlayList
        {
            get => _playList;
            set
            {
                if (_playList != null)
                {
                    _playList.Clear();
                    _playList = null;
                }
                _playList = value;
            }
        }

        public int Current { get => current; set => current = value; }
        private int current = 0;

        private bool interrupted;
        public bool Interrupted
        {
            get => interrupted;
            set => interrupted = value;
        }
        void IAudio.Interrupt(bool i)
        {
            Interrupted = i;
        }

        public IOS_Audio()
        {
            var session = AVAudioSession.SharedInstance();
            session.SetCategory(AVAudioSessionCategory.Playback);
            session.SetActive(true);
        }
        public bool Available()
        {
            return _player != null;
        }

        public void BindViewModel(IAudioViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Close()
        {
            if(_player != null)
            {
                _player.FinishedPlaying -= OnFinished;
                _player.Dispose();
                _player = null;

                if(audioThread.IsAlive)
                {
                    audioThread.Abort();
                }
            }
        }
        public bool IsPlaying()
        {
            if( Available() )
            {
                return _player.Playing;
            }
            else
            {
                return false;
            }
        }

        public void PlayPause()
        {
            if(audioThread == null)
            {
                audioThread = new Thread(Run);
            }
            if( !_player.Playing)
            {
                _player.Play();
                if(!audioThread.IsAlive && audioThread.ThreadState == ThreadState.Unstarted)
                {
                    audioThread.Start();
                }
            }
            else
            {
                _player.Pause();
            }
        }

        public void Prepare(int position)
        {
            if (Current != position)
            {
                Current = position;
            }

            if (_player == null)
            {
                using (var url = NSUrl.FromString(PlayList[current]))
                {
                    NSData data = NSData.FromUrl(url);
                    _player = AVAudioPlayer.FromData(data);
                    _player.FinishedPlaying += OnFinished;
                    _viewModel.Duration = _player.Duration;
                    _viewModel.Position = 0;
                    
                    PlayPause();
                }
            }
            else
            {
                Stop();
            }
        }

        public void SeekTo(double position)
        {
            //_player.PlayAtTime(position );
            _player.CurrentTime = position ;
        }

        public void Stop()
        {
            _viewModel.PlayPause();
            if (_player != null)
            {
                _player.FinishedPlaying -= OnFinished;
                _player.Dispose();
                _player = null;

                if (audioThread != null)
                {
                    audioThread.Abort();
                }
            }
            
            if(PlayList.Count == 1 || PlayList.Count - 1 == current)
            {
                _viewModel.PlayPause();
            }
        }
        private void OnFinished(object sender , AVStatusEventArgs e)
        {
            Stop();
        }

        private void Run()
        {
            while(true)
            {
                if(_player.Playing)
                {
                    Thread.Sleep(1000);
                    if( !Interrupted )
                    {
                        _viewModel.Position = _player.CurrentTime ;
                    }
                }
            }
        }
    }
}