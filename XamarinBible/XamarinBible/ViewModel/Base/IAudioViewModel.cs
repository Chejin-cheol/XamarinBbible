using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBible.ViewModel.Base
{
    public interface IAudioViewModel
    {
        double Position { get; set; }
        double Duration { get; set; }
        void PlayPause();
        void AudioChanged();
    }
}
