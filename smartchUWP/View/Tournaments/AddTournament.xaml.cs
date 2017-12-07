using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace smartchUWP.View.Tournaments
{
    public sealed partial class AddTournament : Page
    {
        public AddTournament() {
            this.InitializeComponent();
            
            AdresseForm.Navigate(typeof(AdresseForm));
            
            
            
        }
        private async void AddTournament_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Address adresse = ((View.AdresseForm)AdresseForm.Content).GetAddress();
            Tournament tournament = new Tournament()
            {
                Address = adresse,
                Name = Name.Text,
                BeginDate = DateDebut.Date.DateTime,
                EndDate = DateFin.Date.DateTime,
                Etat = 0,
                Club = null,
                Participants = null, 
                Admins = null

            };
            TournamentsServices tournamentsServices = new TournamentsServices();
            ResponseObject response = await tournamentsServices.AddTournamentAsync(tournament);
            // this.Frame.Navigate(typeof(AddTournament));
        }
    }
}
