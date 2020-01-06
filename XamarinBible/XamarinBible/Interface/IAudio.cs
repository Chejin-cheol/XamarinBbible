using System;
using System.Collections.Generic;
using System.Text;
using XamarinBible.ViewModel.Base;

namespace XamarinBible.Interface
{
    public interface IAudio 
    {
        List<string> PlayList { get; set; }
        int Current { get; set; }

        void BindViewModel(IAudioViewModel viewModel);
        bool Available();
        void Prepare(int position);
        void PlayPause();
        bool IsPlaying();
        void Stop();
        void Interrupt(bool i);
        void SeekTo(double position);
        void Close();
    }
}
