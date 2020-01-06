//using CarouselView.FormsPlugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinBible.Control;
using XamarinBible.Model;
using XamarinBible.ViewModel;

namespace XamarinBible.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchBiblePage : ContentPage
	{
        NavigationPage _navigation;



        public SearchBiblePage()
        {
            InitializeComponent();
            _navigation = (NavigationPage)App.Current.MainPage;
            _navigation.Popped += OnPoped;
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }



        private void OnPoped(Object sender , NavigationEventArgs e)
        {
            SearchViewModel viewModel = (SearchViewModel)e.Page.BindingContext;
            viewModel.DisposeViewModel();
            BindingContext = null;
            
            _navigation.Popped -= OnPoped;
            _navigation = null;
        }

    }
}