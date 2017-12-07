using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace smartchUWP.View.Tournaments
{
    public sealed partial class Tournaments : Page
    {
        public Tournaments()
        {
            InitializeComponent();
        }

        public void AjouterTournament_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddTournament));
        }


    }
}
