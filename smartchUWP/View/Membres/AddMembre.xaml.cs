using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace smartchUWP.View.Membres
{
    public sealed partial class AddMembre : Page
    {
        public AddMembre() {
            this.InitializeComponent(); 
        }
        private async void AjouterMembre_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            UsersServices usersServices = new UsersServices();

            Address address = new Address()
            {
                Street = Street.Text,
                Number = Number.Text,
                City = City.Text,
                Zipcode = Zipcode.Text,
                Box = Box.Text
            };
            User user = new User()
            {
                Name = Name.Text,
                FirstName = FirstName.Text,
                Email = Email.Text,
                Phone = Phone.Text,
                Birthday = Birthday.Date.DateTime,
                Adresse = address

            };
            ResponseObject response = await usersServices.AddUser(user);
            this.Frame.Navigate(typeof(Membres));
        }
        


    }
}
