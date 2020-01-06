using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinBible.Helper;
using XamarinBible.Interface;
using XamarinBible.Login;
using XamarinBible.Page;

namespace XamarinBible
{
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            InitializeComponent();
        }

        public async void LoginClick(Object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(usernameEntry.Text) || string.IsNullOrEmpty(passwordEntry.Text))
            {
                 await DisplayAlert("아이디/비밀번호를 입력하세요.", "", "확인");
                 return;
            }
            ChangeWaitMode();

            var response = await IdentityService.Login(usernameEntry.Text, passwordEntry.Text);
            if (!string.IsNullOrEmpty(response.Content))
            {
                Settings.AccessToken = response.Content.Replace("\"", "");
                InitAtrribute();
                App.Current.MainPage = new NavigationPage(new MasterTabbedPage());
            }
            else if(response.ResponseStatus == ResponseStatus.Completed)
            {
                 await DisplayAlert("아이디 또는 비밀번호가 일치하지 않습니다.", "", "확인");
            }
            else
            {
                await DisplayAlert("네트워크 상태를 확인해주세요.", "", "확인");
            }
            ChangeWaitMode();
        }

        private void ChangeWaitMode()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                login.IsEnabled = !login.IsEnabled;
                wait.IsVisible = !wait.IsVisible;
                progress.IsRunning = !progress.IsRunning;
            });
        }

        private void InitAtrribute()
        {
            if(Settings.FontSize == 0)
            {
      
                if(Device.RuntimePlatform == Device.Android)
                {
                    Settings.FontSize = Device.GetNamedSize(NamedSize.Large,typeof(Label));
                }
                else
                {
                    Settings.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
                }
            }

            if(Settings.CellSpace == 0)
            {
                Settings.CellSpace = (int)(App.screenHeight * 0.05);
            }
        }


    }


}
