using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XamarinBible.Droid.FromsDependecy;

namespace XamarinBible.Droid.Service
{
    public class AudioServiceConnection : Java.Lang.Object, IServiceConnection
    {
        IAudioItem _dependancy;

        public  AudioServiceConnection(IAudioItem dependancy)
        {
            _dependancy = dependancy;
        }

        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            if(service != null)
            {
                _dependancy.Service = ((AudioServiceBinder)service).GetService();
                _dependancy.Service.View = _dependancy;
                _dependancy.PreparedCallback();
                Console.WriteLine("Service Connected");
            }
        }
        public void OnServiceDisconnected(ComponentName name)
        {
            if (_dependancy != null)
            {
                _dependancy.Service.Stop();
                _dependancy.Service.Dispose();
                _dependancy.Service = null;
                Console.WriteLine("Service Connection Disconnected");
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

        }
    }
}