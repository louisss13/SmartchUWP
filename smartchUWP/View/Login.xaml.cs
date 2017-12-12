using DataAccess;
using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace smartchUWP.View
{
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
        }
        public async void Login_Click(object sender, RoutedEventArgs e)
        {
            AccountsServices accountsServices = new AccountsServices();

            ResponseObject response = await accountsServices.LogIn(Email.Text, Password.Password);

            if (response.Success) {
                ApiAccess.Instance.Token = ((JObject)response.Content)["access_token"].Value<String>();
                TextErreur.Text += response.Content.ToString();
                this.Frame.Navigate(typeof(Clubs.Clubs));
            }
            TextErreur.Visibility = Visibility.Visible;


        }
        public void Register_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Register));
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        
    }
}
