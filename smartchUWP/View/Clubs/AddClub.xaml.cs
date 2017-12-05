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


namespace smartchUWP.View.Clubs
{
    public sealed partial class AddClub : Page 
    {
        public AddClub()
        {
            this.InitializeComponent();
        }

        public async void AddClub_click(object sender, RoutedEventArgs e) {
            ClubsServices clubsServices = new ClubsServices();
            
            Club club = new Club() { Name = ClubName.Text };
            ResponseObject response = await clubsServices.AddClub(club);

            if (response.Success)
            {
                

            }
            else
                TextErreur.Visibility = Visibility.Visible;

        }
    }
}
