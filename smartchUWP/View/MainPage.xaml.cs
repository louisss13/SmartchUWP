using smartchUWP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace smartchUWP.View
{
    public sealed partial class MainPage : Page, IRootFrame
    {
        public MainPage()
        {
            this.InitializeComponent();
            
        }

        public Frame AppFrame { get { return this.ContentFrame; } }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            
            // set the initial SelectedItem 
            foreach (NavigationViewItemBase item in NavView.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "Home")
                {
                    NavView.SelectedItem = item;
                    break;
                }
            }
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(Login));
            }
            else
            {
                switch (args.InvokedItem)
                {
                    case "Home":
                        ContentFrame.Navigate(typeof(Login));
                        break;

                    case "members":
                        ContentFrame.Navigate(typeof(Membres.Membres));
                        break;

                    case "tournaments":
                        ContentFrame.Navigate(typeof(Tournaments.Tournaments));
                        break;

                    case "spectators":
                        ContentFrame.Navigate(typeof(Login));
                        break;

                    case "clubs":
                        ContentFrame.Navigate(typeof(Clubs.Clubs));
                        break;
                }
            }
        }

       

        public Frame getRootFrame()
        {
            return AppFrame;
        }
    }
}
