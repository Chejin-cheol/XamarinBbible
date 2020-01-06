using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamarinBible.Droid.Service
{
    [Service]
    public class AudioService : Android.App.Service 
    {
        private IBinder _binder;
        private IAudioItem _view;
        private MediaPlayer _player;
        private Thread _thread;
        private bool isReleased = false;

        private double position;
        private double duration;
        private bool interrupt;

        public double Position
        {
            get => position;
            set => position = value;
        }
        public double Duration
        {
            get => duration;
            set => duration = value;
        }
        public bool Interrupt
        {
            get => interrupt;
            set => interrupt = value;
        }
        public IAudioItem View
        {
            get => _view;
            set => _view = value;
        }

        public override void OnCreate()
        {
            base.OnCreate();
        }

        public override IBinder OnBind(Intent intent)
        {
            if(_binder == null)
            {
                _binder =new AudioServiceBinder(this);
            }

            return _binder;
        }

        public override bool OnUnbind(Intent intent)
        {
            _binder.Dispose();
            _binder = null;
            return base.OnUnbind(intent);
        }

        public bool Available()
        {
            return _player != null;
        }

        public void Prepare(string url)
        {
            if (_player == null)
            {
                _player = new MediaPlayer();
                _player.SetDataSource(url);
                _player.Completion += OnCompleted;
                if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
                {
                    this._player.SetAudioAttributes(new AudioAttributes.Builder()
                                                       .SetUsage(AudioUsageKind.Media)
                                                       .SetContentType(AudioContentType.Music)
                                                       .SetLegacyStreamType( Stream.Music)
                                                       .Build());
                }
                else
                {
                    this._player.SetAudioStreamType(Stream.Music);
                }
                _thread = new Thread(Run);
                try
                {
                    _player.Prepare();
                }
                catch (Exception e)
                {
                    Console.WriteLine(" &&&&&&&&      :   "+e);
                    Console.WriteLine(" ###########     :      "+url);
                }
                View.Duration = _player.Duration / 1000;
            }
        }

        public bool IsPlaying()
        {
            if ( _player != null && !isReleased)
            {
                return _player.IsPlaying;
            }
            else
            {
                return false;
            }
        }

        public void PlayPause()
        {
            if (!_player.IsPlaying)
            {
                _player.Start();
                if(!_thread.IsAlive)
                {
                    _thread.Start();
                }
            }
            else
            {
                _player.Pause();
            }
        }
        
        public void Stop()
        {
            isReleased = true;
            if (_player != null)
            {
                _thread.Abort();
                _player.Completion -= OnCompleted;
                _player.Stop();
                _player.Release();
                _player = null;
            }
            isReleased = false;
            View.CompletedCollback();
        }

        public void SeekTo(double value)
        {
            if (_player != null)
            {
                _player.SeekTo(((int)value) * 1000);
            }
        }

        public void Run()
        {
            while(true)
            {
                if (_player.IsPlaying)
                {
                    Thread.Sleep(1000);
                    if (!Interrupt)
                    {
                            View.Position =  _player.CurrentPosition / 1000;
                    }
                }
            }
        }

        private void OnCompleted(Object sender , EventArgs e)
        {
            Stop();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected override void Dispose(bool disposing)
        {
            View = null;
            base.Dispose(disposing);
            Console.WriteLine("Service Dispose");
        }
    }
}