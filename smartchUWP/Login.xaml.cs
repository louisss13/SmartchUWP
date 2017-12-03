using DataAccess;
using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace smartchUWP
{
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
        }
        public async void Login_Click(object sender, RoutedEventArgs e)
        {
            UsersServices userService = new UsersServices();

            ResponseObject response = await userService.LogIn(Email.Text, Password.Password);

            if (response.Success) {
                ApiAccess.Instance.Token = ((JObject)response.Content)["access_token"].Value<String>();
                TextErreur.Text += response.Content.ToString();

            }
            TextErreur.Visibility = Visibility.Visible;


        }
        public void Register_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
