using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinBible.ViewModel;

namespace XamarinBible.Model
{
    public class HymnModel : BaseViewModel
    {

        private HymnViewModel outerViewModel = null;
        public ICommand ItemClickCommand { get; protected set; }
        private int position;

        public HymnModel(HymnViewModel vm)
        {
            outerViewModel = vm;
        }

        public HymnViewModel OuterViewModel
        {
            get
            {
                return outerViewModel;
            }

            protected set
            {
                outerViewModel = value;
            }
        }

        public int Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }
    }
}
