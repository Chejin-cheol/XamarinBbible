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

namespace XamarinBible.Droid.Service
{
    public class AudioServiceBinder : Binder
    {
        AudioService _service;
        public AudioServiceBinder(AudioService service)
        {
            _service = service;
        }
        public AudioService GetService()
        {
            return _service;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Console.WriteLine("Binder Disposed");
        }
    }
}