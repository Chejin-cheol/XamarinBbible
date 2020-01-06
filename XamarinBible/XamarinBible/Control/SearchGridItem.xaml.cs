using AiForms.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinBible.Model;

namespace XamarinBible.Control
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchGridItem : StackLayout
	{
        RepeatableWrapLayout layout;
        public SearchGridItem ()
		{
			InitializeComponent();
            layout = Children as RepeatableWrapLayout;
        }
    }
}