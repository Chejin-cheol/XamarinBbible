using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBible.ViewModel
{
    public class DetailViewModel : BaseViewModel
    {
        private TabbedPageViewModel masterViewModel;
        public TabbedPageViewModel MasterViewModel{ get => masterViewModel; set => masterViewModel = value; }
    }
}
