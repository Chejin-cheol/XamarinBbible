using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using XamarinBible.Model;

namespace XamarinBible.DI.Service
{
    public class SermonService : ISermonService
    {
        public ObservableCollection<Sermon> GetList(int book, int chapter)
        {
            var client = new RestClient("http://xamarin.gntc.net/api/sermon");
            var request = new RestRequest("list", Method.GET);
            request.AddQueryParameter("book", book.ToString());
            request.AddQueryParameter("chapter",  chapter.ToString());
            IRestResponse response = client.Execute(request);

            response.Content = response.Content.Replace("\\","").TrimStart('\"').TrimEnd('\"'); 
            JArray jarray = JArray.Parse(response.Content);

            ObservableCollection<Sermon> sermons = new ObservableCollection<Sermon>();
            foreach (JObject obj in jarray)
            {
                Console.WriteLine(obj["title"].ToString()+"   &&&");
                sermons.Add(new Sermon()
                {
                    Title = obj["title"].ToString(),
                    Date = obj["date"].ToString(),
                    StartParagraph = int.Parse(obj["start"].ToString()),
                    EndParagraph = int.Parse(obj["end"].ToString())
                });
            }

            return sermons;
        }
    }
}
