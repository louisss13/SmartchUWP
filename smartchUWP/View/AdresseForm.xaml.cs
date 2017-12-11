using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace smartchUWP.View
{
    public sealed partial class AdresseForm : Page 
    {
        public AdresseForm()
        {
            this.InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)

        {

            Frame frame = sender as Frame;

            (frame.Content as AdresseForm).DataContext = frame.DataContext;

        }

        public Address GetAddress() {
            Address address = new Address()
            {
                Street = Street.Text,
                Number = Number.Text,
                City = City.Text,
                Zipcode = Zipcode.Text,
                Box = Box.Text
            };
            return address;
        }

        internal void SetStreet(string v)
        {
            Street.Text = v;
        }
    }
}
