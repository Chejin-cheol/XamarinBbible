using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinBible.Model;

namespace XamarinBible.Control
{
    class HymnTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ListTemplate { get; set; }
        public DataTemplate GridTemplate { get; set; }
        public DataTemplate AlbumTemplate { get; set; }

        public HymnTemplateSelector()
        {
            ListTemplate = new DataTemplate(typeof(HymnListView));
            GridTemplate = new DataTemplate(typeof(HymnDialView));
            AlbumTemplate = new DataTemplate(typeof(HymnAlbumView));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {

            switch(((HymnModel)item).Position)
            {
                case 0:
                    return GridTemplate;
                case 1:
                    return ListTemplate;
                default :
                    return AlbumTemplate;
            }
        }
    }
}
