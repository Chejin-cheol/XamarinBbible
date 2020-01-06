using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinBible.Model;
using XamarinBible.Page;

namespace XamarinBible.Control
{
    public class SearchTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ListTemplate { get; set; }
        public DataTemplate GridTemplate { get; set; }

        public SearchTemplateSelector()
        {
            ListTemplate = new DataTemplate(typeof(SearchListView));
            GridTemplate = new DataTemplate(typeof(SearchGridItem));
        }
        
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if( ((SearchModel)item).Position >0 )
            {
                return GridTemplate;
            }
            else
            {
                return ListTemplate;
            }
        }
    }
}
