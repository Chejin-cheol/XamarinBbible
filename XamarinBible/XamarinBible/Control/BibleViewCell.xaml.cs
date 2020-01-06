using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinBible.Helper;
using XamarinBible.Model;

namespace XamarinBible.Control
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BibleViewCell : ViewCell
	{

        public Label Sentence;
        public BibleViewCell ()
		{
            InitializeComponent ();
            Sentence = sentence;
		}

    }
}