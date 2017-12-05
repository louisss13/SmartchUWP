using DataAccess;
using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace smartchUWP.View
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class Register : Page
    {
        public Register()
        {
            this.InitializeComponent();
        }
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            AccountsServices accountsServices = new AccountsServices();
            Account newUser = new Account() { Password = Password.Password, Email=Email.Text};
            ResponseObject AddedUser = await accountsServices.AddUser(newUser);
            if (AddedUser.Success)
            {
                messageOk.Visibility = Visibility.Visible;
                messageNotOk.Visibility = Visibility.Collapsed;
            }
            else
            {
                messageOk.Visibility = Visibility.Collapsed;
                messageNotOk.Text += AddedUser.Content;
                messageNotOk.Visibility = Visibility.Visible;

            }
            //this.Frame.Navigate(typeof(Page2));
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            //this.Frame.Navigate(typeof(Page2));
        }
    }
}
