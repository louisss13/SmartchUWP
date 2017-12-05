using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace smartchUWP.View.Clubs
{
    public sealed partial class Clubs : Page
    {
        public Clubs()
        {
            this.InitializeComponent();
        }

        private void AddClub_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddClub));
        }
    }
}
