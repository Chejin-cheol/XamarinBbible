using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using XamarinBible.Droid.FromsDependecy;
using XamarinBible.Droid.Service;
using XamarinBible.Interface;
using XamarinBible.ViewModel.Base;

[assembly: Dependency(typeof(Android_Audio))]
namespace XamarinBible.Droid.FromsDependecy
{
    public class Android_Audio : IAudio , IAudioItem
    {
        private IAudioViewModel _viewmodel;
        private AudioService _service;
        private AudioServiceConnection _conection;
        private Intent _intent;

        public AudioService Service
        {
            get => _service;
            set => _service = value;
        }

        private List<string> _playList;
        public  List<string> PlayList
        {
            get => _playList;
            set
            {
                if(_playList != null)
                {
                    _playList.Clear();
                    _playList = null;
                }
                _playList = value;
            }
        }

        public int Position {  set => _viewmodel.Position = value; }
        public int Duration { set => _viewmodel.Duration =value; }
        public int Current { get => current; set => current = value; }

        private int current;
        
        public void BindViewModel(IAudioViewModel viewModel)
        {
            _viewmodel = viewModel;
        }

        public bool Available()
        {
            if( Service == null )
            {
                return false;
            }
            else if(! Service.Available() )
            {
                return false;
            }
            return true;
        }

        public void Prepare(int position )
        {
            if(current != position)
            {
                current = position;
            }           
            
            if (Service == null)
            {           
                _conection = new AudioServiceConnection(this);
                _intent = new Intent(MainActivity._context, typeof(AudioService));
                MainActivity._context.BindService(_intent, _conection, Bind.AutoCreate);
            }
            else
            {
                if(Service.IsPlaying())
                {
                    Stop();
                }
                PreparedCallback();
            }
        }

        public bool IsPlaying()
        {
            if (Service != null)
            {
                return Service.IsPlaying();
            }
            return false;
        }

        public void PlayPause()
        {
            Service.PlayPause();
        }

        public void Stop()
        {
            Service.Stop();
        }

        public void Close()
        {
            if (Service != null)
            {
                _playList.Clear();
                _playList = null;
                _viewmodel.Position = 0;
                Service.Stop();
                MainActivity._context.UnbindService(_conection);
                MainActivity._context.StopService(_intent);
                Service = null;
            }
        }

        public void SeekTo(double position)
        {
            if (Service != null)
            {
                Service.SeekTo(position);
            }
        }

        public void Interrupt(bool i)
        {
            if (Service != null)
            {
               Service.Interrupt = i;
            }
        }
        
        //  callback
        public void PreparedCallback()
        {
            if(Service != null)
            {
                Service.Prepare(_playList[current]);
                Service.PlayPause();
                _viewmodel.AudioChanged();
            }
            current++;  
        }

        public void CompletedCollback()
        {
            if (_playList != null)
            {
                if (current <= _playList.Count - 1 && (_playList.Count - 1) != 0)
                {
                    _viewmodel.Position = 0;
                    Prepare(current);
                }
                else
                {
                    current = 0;
                    _viewmodel.PlayPause();
                }
            }
        }
    }
}