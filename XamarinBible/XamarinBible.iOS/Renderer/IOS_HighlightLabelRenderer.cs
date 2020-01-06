using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinBible.Control;
using XamarinBible.iOS.Renderer;

[assembly: ExportRenderer(typeof(HighlightLabel), typeof(IOS_HighlightLabelRenderer))]
namespace XamarinBible.iOS.Renderer
{
    public class IOS_HighlightLabelRenderer : LabelRenderer
    {
        UIStringAttributes attributes;

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);


            if((e.OldElement != null) || (this.Element == null))
            {
                return;
            }

            if(Control != null)
            {
                Control.AttributedText = new NSMutableAttributedString(Control.Text);
                attributes = new UIStringAttributes();
            }
        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);


            if(e.PropertyName == "Renderer")
            {
                UpdateItemState();
            }


            if (e.PropertyName == "Dash")
            {
                if (((HighlightLabel)Element).Dash)
                {
                    attributes.UnderlineStyle = NSUnderlineStyle.Single;
                }
                else
                {
                    attributes.UnderlineStyle = NSUnderlineStyle.None;
                }
                Control.AttributedText = new NSMutableAttributedString(Control.Text, attributes);
            }

            if (e.PropertyName == "Highlight")
            {
                if (((HighlightLabel)Element).Highlight != null)
                {
                    var color = Color.FromHex(((HighlightLabel)Element).Highlight).ToUIColor();
                    attributes.BackgroundColor = color;
                }
                else
                {
                    attributes.BackgroundColor = UIColor.White;
                }
                Control.AttributedText = new NSMutableAttributedString(Control.Text, attributes);
            }

            if(e.PropertyName =="CellBackgroudColor")
            {
                Color color = ((HighlightLabel)Element).BackgroundColor;

                if(color.Equals(Color.White))
                {
                    attributes.BackgroundColor = UIColor.White;
                }
                else
                {
                    attributes.BackgroundColor = color.ToUIColor();
                }
            }

            SetNeedsDisplay();
            
        }



        private void UpdateItemState()
        {
            var paragraphStyle = new NSMutableParagraphStyle { LineSpacing = 100 };
            if( ((HighlightLabel)Element).Dash )
            {
                attributes.UnderlineStyle = NSUnderlineStyle.Single;
            }
            else
            {
                attributes.UnderlineStyle = NSUnderlineStyle.None;
            }


            if (((HighlightLabel)Element).Highlight != null)
            {
                var color = Color.FromHex(((HighlightLabel)Element).Highlight).ToUIColor();
                attributes.BackgroundColor = color;
            }
            else
            {
                attributes.BackgroundColor = UIColor.White;
            }

        }
    }
}