using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinBible.Login;
using XamarinBible.Page;

namespace XamarinBible
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public  void LoginClick(Object sender, EventArgs e)
        {
            //        await Navigation.PushModalAsync(new MasterTabbedPage());
            Console.WriteLine("오야야애ㅑㅇ농랴나룽나ㅓ룽나ㅜㄹㄴㅇ");
                  Login(usernameEntry.Text , passwordEntry.Text);
           
        }


        public void Login(string _id , string _password)
        {
    
            var client =new  RestClient("https://xamarin.gntc.net/api/");
            var request = new RestRequest("token", Method.POST);

            request.AddParameter("grant_type", "password");
            request.AddParameter("username", _id);
            request.AddParameter("password", _password);
   
            IRestResponse response = client.Execute(request);
          
      //      LoginToken token = JsonConvert.DeserializeObject<LoginToken>(response.Content);

            Console.WriteLine(response.Content);
        
/*

            if (token.id_token != null)
            {
                Application.Current.Properties["id_token"] = token.id_token;
                Application.Current.Properties["access_token"] = token.access_token;
                //                GetUserData(token.id_token);
            }
            else
            {
                DisplayAlert("Oh No!", "It's seems that you have entered an incorrect email or password. Please try again.", "OK");
            }
            */
           
        }


        public class LoginToken
        {
            public string id_token { get; set; }
            public string access_token { get; set; }
            public string token_type { get; set; }
        }

    }


}
