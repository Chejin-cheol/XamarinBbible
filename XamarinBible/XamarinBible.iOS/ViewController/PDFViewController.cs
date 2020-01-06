using System;
using System.IO;
using CoreGraphics;
using Foundation;
using PdfKit;
using UIKit;
using Xamarin.Forms;

namespace XamarinBible.iOS.ViewController
{
    public class PDFViewController : UIViewController
    {
        string _category, _fileName, _page;
        PdfView _pdfView;

        UIView _musicBar ,_sliderBar ,_timeBar ,_playBar; 
        UILabel _position, _duration;
        UISlider _slider;

        public PDFViewController(string category, string fileName, string page)
        {
            _category = category;
            _fileName = fileName;
            _page = page;
        }

        private void SetLayout()
        {
            _pdfView = new PdfView();
            _pdfView.BackgroundColor = UIColor.White;
            Add(_pdfView);
            _pdfView.TranslatesAutoresizingMaskIntoConstraints = false;
            _pdfView.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor).Active = true;
            _pdfView.HeightAnchor.ConstraintEqualTo(View.HeightAnchor, new nfloat(0.8)).Active = true;
            _pdfView.WidthAnchor.ConstraintEqualTo(View.WidthAnchor).Active = true;

            _musicBar = new UIView();            
             View.Add(_musicBar);
            _musicBar.BackgroundColor = new UIColor( (nfloat)Xamarin.Forms.Color.FromHex("#343434").R , (nfloat)Xamarin.Forms.Color.FromHex("#343434").G , (nfloat)Xamarin.Forms.Color.FromHex("#343434").B , 1) ;
            _musicBar.TranslatesAutoresizingMaskIntoConstraints = false;
            _musicBar.TopAnchor.ConstraintEqualTo(_pdfView.BottomAnchor).Active = true;
            _musicBar.BottomAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.BottomAnchor).Active = true;
            _musicBar.WidthAnchor.ConstraintEqualTo(View.WidthAnchor).Active = true;
        }

        private void LoadPdfDocs()      
        {
            var folderPath = Path.Combine(path1: System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments)
                             , path2: "..", path3: "Library", path4: _category);
            var filePath = Path.Combine(folderPath, _fileName);
            var pdfDocument = new PdfDocument(NSUrl.FromFilename(filePath));
            _pdfView.AutoScales = true;
            _pdfView.AutosizesSubviews = true;
            _pdfView.MaxScaleFactor = new nfloat(2.0);
            _pdfView.MinScaleFactor = _pdfView.ScaleFactorForSizeToFit;
            _pdfView.DisplayMode = PdfDisplayMode.SinglePageContinuous;
            _pdfView.ScaleFactor = _pdfView.MinScaleFactor;
            _pdfView.DisplayDirection = PdfDisplayDirection.Horizontal;
            _pdfView.UsePageViewController(true , null);

            _pdfView.Document = pdfDocument;
            _pdfView.DisplaysPageBreaks = false;
            _pdfView.Subviews[0].BackgroundColor = UIColor.White;
        }

        private void SetLayoutSubViews()
        {
            _sliderBar = new UIView();
            _slider = new UISlider();
            _timeBar = new UIView();

            _musicBar.Add(_sliderBar);
            _sliderBar.TranslatesAutoresizingMaskIntoConstraints = false;
            _sliderBar.TopAnchor.ConstraintEqualTo( _musicBar.TopAnchor ).Active = true;
            _sliderBar.WidthAnchor.ConstraintEqualTo(_musicBar.WidthAnchor).Active = true;
            _sliderBar.Add(_slider);

            _slider.TranslatesAutoresizingMaskIntoConstraints = false;
            _slider.TopAnchor.ConstraintEqualTo(_sliderBar.TopAnchor).Active = true;
            _slider.WidthAnchor.ConstraintEqualTo(_sliderBar.WidthAnchor).Active = true;

            _timeBar = new UIView();
            _sliderBar.Add(_timeBar);
            _timeBar.TranslatesAutoresizingMaskIntoConstraints = false;
            _timeBar.BottomAnchor.ConstraintEqualTo(_sliderBar.BottomAnchor).Active = true;
            _timeBar.WidthAnchor.ConstraintEqualTo(_sliderBar.WidthAnchor).Active = true;
            _timeBar.HeightAnchor.ConstraintEqualTo(_sliderBar.HeightAnchor, new nfloat(0.4)).Active = true;

            _position = new UILabel();
            _position.Text = "00:00";
            _position.TextColor = UIColor.White;
            _position.TextAlignment = UITextAlignment.Center;

            _duration = new UILabel();
            _duration.TextColor = UIColor.White;
            _duration.Text = "00:00";
            _duration.TextAlignment = UITextAlignment.Center;

            _timeBar.Add(_position);
            _timeBar.Add(_duration);

            _position.TranslatesAutoresizingMaskIntoConstraints = false;
            _position.LeadingAnchor.ConstraintEqualTo(_timeBar.LeadingAnchor).Active = true;
            _position.HeightAnchor.ConstraintEqualTo(_timeBar.HeightAnchor).Active = true;

            _duration.TranslatesAutoresizingMaskIntoConstraints = false;
            _duration.TrailingAnchor.ConstraintEqualTo(_timeBar.TrailingAnchor).Active = true;
            _duration.HeightAnchor.ConstraintEqualTo(_timeBar.HeightAnchor).Active = true;

            _playBar = new UIView();
            _musicBar.Add(_playBar);
            _playBar.TranslatesAutoresizingMaskIntoConstraints = false;
            _playBar.TopAnchor.ConstraintEqualTo(_sliderBar.BottomAnchor).Active = true;
            _playBar.WidthAnchor.ConstraintEqualTo(_musicBar.WidthAnchor).Active = true;
            _playBar.BackgroundColor = UIColor.Gray;
            _playBar.HeightAnchor.ConstraintEqualTo(_musicBar.HeightAnchor, new nfloat(0.6)).Active = true;
            _playBar.BottomAnchor.ConstraintEqualTo(_musicBar.SafeAreaLayoutGuide.BottomAnchor).Active = true;

        }

        public override void ViewWillTransitionToSize(CGSize toSize, IUIViewControllerTransitionCoordinator coordinator)
        {
            base.ViewWillTransitionToSize(toSize, coordinator);
            _pdfView.Subviews[0].SetNeedsDisplay();
            _pdfView.Subviews[0].SetNeedsLayout();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetLayout();
            SetLayoutSubViews();
            LoadPdfDocs();
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            _pdfView.GoToPage(_pdfView.Document.GetPage(int.Parse(_page) - 1));
            _pdfView.Subviews[0].SetNeedsDisplay();
            _pdfView.Subviews[0].SetNeedsLayout();
        }
    }
}
