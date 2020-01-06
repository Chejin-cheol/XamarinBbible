using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinBible.Control
{
    public class CircleButton : Button
    {
        public CircleButton()
        {
            if(WidthRequest > HeightRequest)
            {
                WidthRequest = 50;
                CornerRadius = (int)(HeightRequest / 2);
            }
            else
            {
                HeightRequest = 40;
                HeightRequest = (int)(WidthRequest / 2);
            }
                
        }

    }
}
