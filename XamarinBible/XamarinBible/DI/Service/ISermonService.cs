using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using XamarinBible.Model;

namespace XamarinBible.DI.Service
{
    public interface ISermonService
    {
        ObservableCollection<Sermon> GetList(int book ,int chapter);
    }
}
