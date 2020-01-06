using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinBible.Helper;

namespace XamarinBible.Control
{
    public class HighlightLabel : Label
    {
        public static readonly BindableProperty DashProperty = BindableProperty.Create(nameof(Dash), typeof(bool), typeof(HighlightLabel), false, propertyChanged: OnContentChanged);
        public static readonly BindableProperty HighlightProperty = BindableProperty.Create(nameof(Highlight), typeof(string), typeof(HighlightLabel), null, propertyChanged: OnContentChanged);
        public static readonly BindableProperty BaseContextProperty = BindableProperty.Create("BaseContext", typeof(object), typeof(HighlightLabel), null);



        public object BaseContext
        {
            get { return GetValue(BaseContextProperty); }
            set { SetValue(BaseContextProperty, value); }
        }

        public bool Dash
        {
            get { return (bool)GetValue(DashProperty); }
            set { SetValue(DashProperty, value); }
        }
        public string Highlight
        {
            get { return (string)GetValue(HighlightProperty); }
            set { SetValue(HighlightProperty, value); }
        }
        


        public static void OnContentChanged(BindableObject bindable, object oldValue, object newValue)
        {
           
            if (bindable != null)
            {
            }
        }
        

        /*

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(HighlightLabel), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(Object), typeof(HighlightLabel), null);

        public Object CommandParameter
        {
            get { return (Object)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

    */
    }
}
