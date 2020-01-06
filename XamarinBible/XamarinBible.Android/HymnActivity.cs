using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Artifex.MuPdfDemo;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinBible.Database;
using XamarinBible.Droid.FromsDependecy;
using XamarinBible.Droid.Service;
using XamarinBible.Droid.Widget;
using XamarinBible.Interface;
using static Android.Widget.SeekBar;
using static Com.Artifex.MuPdfDemo.ReaderView;

namespace XamarinBible.Droid
{
    [Activity(LaunchMode = Android.Content.PM.LaunchMode.SingleTop , Theme = "@android:style/Theme.NoTitleBar" ,ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize | Android.Content.PM.ConfigChanges.KeyboardHidden ,HardwareAccelerated =true ) ]
    public class HymnActivity : Activity  , IAudioItem  
    {
        private MuPDFCore _core;
        private MuPDFReaderView mDocView;
        private MuPDFPageAdapter mAdapter;
        Android.Widget.RelativeLayout mPDFView;
        
        // ui 객체
        private SeekBar seekBar;
        private ViewGroup playBar;
        private ImageView next ,player ,prev;
        private TextView positionText, durationText;

        //db 
        HymnDataAccess _hymnData;

        //서비스
        private bool isPlaying;
        private int currentHymnNo;
        private Handler _handler;
        private Intent _serviceIntent;
        private AudioServiceConnection _connection;
        private AudioService _service;
        public AudioService Service { get => _service; set => _service = value; }
        public int Position
        {
            set
            {
                _handler.SendEmptyMessage(value);
            }
        }
        public int Duration
        {
            set
            {
                Message msg = _handler.ObtainMessage();
                msg.Arg1 = 1;
                msg.What = value;
                _handler.SendMessage(msg);
            }
        }

        private List<string> _playList = new List<string>();
        private string category;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.hymn_layout);

            //db
            _hymnData = HymnDataAccess.Instance();

            //extra

            var page = int.Parse(Intent.GetStringExtra("page")) - 1;
            category = Intent.GetStringExtra("category");
            var fileName = Intent.GetStringExtra("fileName");
            var path = new Android_Path();
            Android.Net.Uri file_path = null;
            file_path = Android.Net.Uri.Parse("file://" + path.getLocalPath(category, fileName));
            var uri = Android.Net.Uri.Decode(file_path.EncodedPath);

            //mupdf setting
            mPDFView = (Android.Widget.RelativeLayout)FindViewById(Resource.Id.pdfView);
            _core = openFile(uri);
            setMuPDFView(page);
            
            //ui
            SetView();
            SetLayout();
            SetHandler();
        }
        
        //layout 영역
        public void SetView()
        {
            seekBar = (SeekBar)FindViewById(Resource.Id.seekbar);
            playBar = (ViewGroup)FindViewById(Resource.Id.playbar);
            player = (ImageView)FindViewById(Resource.Id.player);
            prev = (ImageView)FindViewById(Resource.Id.prev);
            next = (ImageView)FindViewById(Resource.Id.next);
            positionText = (TextView)FindViewById(Resource.Id.position);
            durationText = (TextView)FindViewById(Resource.Id.duration);

            seekBar.StartTrackingTouch += SeekbarTouchDown;
            seekBar.StopTrackingTouch += SeekbarTouchUp;
            player.Click += OnClick;
            prev.Click += MoveToPrev;
            next.Click += MoveToNext;
        }

        public void SetLayout()
        {
            var metrics = Resources.DisplayMetrics;
            int parent = metrics.HeightPixels / 8;
            int seekbarHeight = (int) (parent * 0.2) ;
            int timerTextHeight = (int)(parent * 0.25);
            float timerTextSize = timerTextHeight * 0.8f;
            int iconSize = (int)(parent * 0.45);
            int playerMargin = (int)(metrics.WidthPixels * 0.1);
            
            LinearLayout.LayoutParams seekParam = (LinearLayout.LayoutParams)seekBar.LayoutParameters;
            seekParam.SetMargins(0, 0, 0, 0);
            seekParam.Height = seekbarHeight;
            seekBar.LayoutParameters = seekParam;
            seekBar.SetPadding(0, 0, 0, 0);

            ViewGroup timerTextGroup = (ViewGroup)FindViewById(Resource.Id.timerTextGoup);
            LinearLayout.LayoutParams timerParams = (LinearLayout.LayoutParams)timerTextGroup.LayoutParameters;
            timerParams.Height = timerTextHeight;
            timerTextGroup.LayoutParameters = timerParams;
            positionText.SetTextSize(ComplexUnitType.Px, timerTextSize);
            positionText.SetPadding(10, 0 ,0 , 0);
            durationText.SetTextSize(ComplexUnitType.Px, timerTextSize);
            durationText.SetPadding(10, 0, 0, 0);

            playBar.SetPadding(0,0,0,0);
            player.LayoutParameters.Height = iconSize;
            player.LayoutParameters.Width = iconSize;
            ((Android.Widget.RelativeLayout.LayoutParams)player.LayoutParameters).SetMargins(playerMargin, 0, playerMargin, 0);
            next.LayoutParameters.Height = iconSize;
            next.LayoutParameters.Width = iconSize;
            prev.LayoutParameters.Height = iconSize;
            prev.LayoutParameters.Width = iconSize;
        }

        public void SetHandler()
        {
            _handler = new ProgressHandler(seekBar , positionText ,durationText);
        }
       

        private void PlayerIconChange()
        {
            if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.LollipopMr1)
            {
                if (!isPlaying)
                {
                    player.SetImageDrawable(Resources.GetDrawable(Resource.Drawable.ic_player_pause, ApplicationContext.Theme));
                }
                else
                {
                    player.SetImageDrawable(Resources.GetDrawable(Resource.Drawable.ic_player_play, ApplicationContext.Theme));
                }
            }
            else
            {
                if (!isPlaying)
                {
                    player.SetImageDrawable(Resources.GetDrawable(Resource.Drawable.ic_player_pause));
                }
                else
                {
                    player.SetImageDrawable(Resources.GetDrawable(Resource.Drawable.ic_player_pause));
                }
            }
            isPlaying = !isPlaying;
        }

        // event 
        public void OnClick(object sender , EventArgs e)
        {
            if(Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                DependencyService.Get<IMessage>().LongAlert("인터넷 상태를 확인해주세요.");
                return;
            }
            
                PlayerIconChange();
                if (Service == null)
                {
                    Prepare();
                }
                else if(Service.Available())
                {
                    Service.PlayPause();
                }
                else
                {
                    Prepare();
                }
         }

        public void MoveToPrev(object sender, EventArgs e)
        {
            int page = _hymnData.GetPage((currentHymnNo -1).ToString());
            mDocView.DisplayedViewIndex = page - 1;
        }
        public void MoveToNext(object sender, EventArgs e)
        {
            int page = _hymnData.GetPage((currentHymnNo +1).ToString());
            mDocView.DisplayedViewIndex = page - 1;
        }

        public async void PageChanged(int page)
        {
            if (Service != null)
            {
                if (Service.IsPlaying())
                {
                    string url="";
                    await
                    Task.Run(() =>
                    {
                        int curPage = page + 1;
                        int hymnNo = _hymnData.GetHymnNo(curPage);
                        if (currentHymnNo == hymnNo)
                            return;
                        Service.Stop();
                        currentHymnNo = hymnNo;
                        url = string.Format(XamarinBible.Constants.SermonAddress, category, currentHymnNo);
                        Service.Prepare(url);
                        Service.PlayPause();
                    });
                }
                else
                {
                    int curPage = page + 1;
                    currentHymnNo = _hymnData.GetHymnNo(curPage);
                }
            }
            else
            {
                int curPage = page + 1;
                currentHymnNo = _hymnData.GetHymnNo(curPage);
            }
        }

        //seekbar event
        private void SeekbarTouchDown(object sender , StartTrackingTouchEventArgs e)
        {
            if (Service != null)
            {
                Service.Interrupt = true;
            }
        }
        private void SeekbarTouchUp(object sender , StopTrackingTouchEventArgs e)
        {
            if (Service != null)
            {
                int value = seekBar.Progress;
                _handler.SendEmptyMessage(value);
                Service.SeekTo(value);
                Service.Interrupt = false;
            }
        }


        //prepare
        private void Prepare()
        {
            if (Service == null)
            {
                _connection = new AudioServiceConnection(this);
                _serviceIntent = new Intent(this, typeof(AudioService));
                BindService(_serviceIntent, _connection, Bind.AutoCreate);
            }
            else
            {
                if (Service.IsPlaying())
                    Service.Stop();
                PreparedCallback();
            }
        }

        //Service Callback
        public void PreparedCallback()
        {
            int curPage = mDocView.DisplayedViewIndex + 1;
            currentHymnNo = _hymnData.GetHymnNo(curPage);
            string url = string.Format("http://n.gntc.net/DATA/{0}/ar/{1}.mp3", category, currentHymnNo);
            Service.Prepare(url);
            Service.PlayPause();
        }

        public void CompletedCollback()
        {
            if(seekBar != null)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
               {
                   seekBar.Progress = 0;
                   positionText.Text = "00:00";
               });
            }
        }
        
        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            if (newConfig.Orientation == Android.Content.Res.Orientation.Portrait)
            {
                playBar.Visibility = ViewStates.Visible;
            }

            if (newConfig.Orientation == Android.Content.Res.Orientation.Landscape)
            {
                playBar.Visibility = ViewStates.Invisible;
            }
        }
        
        //  ***** MuPDF setting
        private MuPDFCore openFile(String path)
        {
            try
            {
                MuPDFCore core = new MuPDFCore(this, path);
                OutlineActivityData.Set(null);

                return core;
            }
            catch (Exception e)
            {
                Console.WriteLine("MuPDFCore-openFile: (error)" + e);

                return null;
            }
        }
        
        public void setMuPDFView(int page)
        {
            if (_core == null)
                return;

            // Now create the UI.
            // First create the document view
            mDocView = new XFMuPDFReaderView(this);
            mAdapter = new MuPDFPageAdapter(this, _core);
            mDocView.SetAdapter(mAdapter);
            mDocView.DisplayedViewIndex = page;

            // Stick the document view and the buttons overlay into a parent view

            if (mPDFView != null)
            {
                    mPDFView.AddView(mDocView);
            }
        }

        protected override void OnDestroy()
        {
            ClearPDF();           
            ReleaseInstance();
            
            if(_core != null)
            {
                _core.OnDestroy();
            }

            base.OnDestroy();
            
            Dispose();
            GC.Collect();
        }
        private void ClearPDF()
        {
            if (mDocView != null)
            {
                DisposeReaderView();


                mDocView.Dispose();
                mPDFView.Dispose();
                mAdapter.ReleaseBitmaps();
                mAdapter.Dispose();


                if (_core != null)
                {
                    _core.OnDestroy();
                    _core.Dispose();
                    _core = null;
                    mAdapter = null;
                    mDocView = null;
                    mPDFView = null;
                }
            }
        }
        
        void DisposeReaderView()
        {

            for (int i = 0; i < mDocView.ChildCount; i++)
            {
                MuPDFPageView mupdfPageView = (MuPDFPageView)mDocView.GetChildAt(i);
                ((IMuPDFView)mupdfPageView).ReleaseBitmaps();
                ((IMuPDFView)mupdfPageView).ReleaseResources();

                if (mDocView.MChildViews.ValueAt(i) != mupdfPageView)
                {
                    var view = (IMuPDFView)mDocView.MChildViews.ValueAt(i);
                    view.ReleaseBitmaps();
                    view.ReleaseResources();
                    view.Dispose();
                    view = null;

                }

                for (int j = 0; j < mupdfPageView.ChildCount; j++)
                {
                    var pageItem = mupdfPageView.GetChildAt(j);
                    if (pageItem is ImageView)
                    {
                        ImageView img = ((ImageView)pageItem);
                        img.DestroyDrawingCache();
                        img.Drawable.SetCallback(null);
                        img.SetImageBitmap(null);
                        img.Dispose();
                        img = null;
                    }
                    pageItem.Dispose();
                    pageItem = null;
                }
                mupdfPageView.Dispose();
                mupdfPageView = null;
            }

            mDocView.DestroyDrawingCache();
            mDocView.MChildViews.Dispose();
            mDocView.MChildViews = null;

            mDocView.MGestureDetector.Dispose();
            mDocView.MStepper.Dispose();
            mDocView.MScroller.Dispose();
            mDocView.MScaleGestureDetector.Dispose();

            mDocView.MGestureDetector = null;
            mDocView.MStepper = null;
            mDocView.MScroller = null;
            mDocView.MScaleGestureDetector = null;
        }

        //dispose
        protected override void Dispose(bool disposing)
        {
            seekBar.StartTrackingTouch -= SeekbarTouchDown;
            seekBar.StopTrackingTouch -= SeekbarTouchUp;
            seekBar.Dispose();
            seekBar = null;

            positionText.Dispose();
            positionText = null;

            durationText.Dispose();
            durationText = null;

            player.Click -= OnClick;
            player.Dispose();
            player = null;
            prev.Dispose();
            prev = null;
            next.Dispose();
            next = null;

            playBar.Dispose();
            playBar = null;

            _handler.Dispose();
            _handler = null;

            if (Service != null)
            {
                CompletedCollback();
                Service.Stop();
                UnbindService(_connection);
                StopService(_serviceIntent);
            }

            _playList.Clear();
            _playList = null;
            _hymnData = null;

            base.Dispose(disposing);
            GC.Collect();
        }
    }

    class ProgressHandler : Handler
    {
        private TextView _progress , _duration;
        private SeekBar _seekbar;
        public ProgressHandler(SeekBar sk , TextView p ,TextView d)
        {
            _seekbar = sk;
            _progress = p;
            _duration = d;
        }
        public override void HandleMessage(Message msg)
        {
            base.HandleMessage(msg);

            int minute = msg.What / 60;
            int second = msg.What % 60;
            if (msg.Arg1 == 1)
            {
                _seekbar.Max = msg.What;
                _duration.Text = string.Format("{0:D2}:{1:D2}", minute, second);
            }
            else
            {
                _seekbar.Progress = msg.What;
                _progress.Text = string.Format("{0:D2}:{1:D2}", minute, second);
            }
        }
        
        protected override void Dispose(bool disposing)
        {
            _seekbar.Dispose();
            _seekbar = null;
            _progress.Dispose();
            _progress = null;
            _duration.Dispose();
            _duration = null;
            base.Dispose(disposing);
        }
    }
}