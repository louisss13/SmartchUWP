using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace smartchUWP.View
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            // you can also add items in code behind
           // NavView.MenuItems.Add(new NavigationViewItemSeparator());
            //NavView.MenuItems.Add(new NavigationViewItem()
            //{ Content = "My content", Icon = new SymbolIcon(Symbol.Folder), Tag = "content" });

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
                        ContentFrame.Navigate(typeof(Login));
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

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                ContentFrame.Navigate(typeof(Login));
            }
            else
            {

                NavigationViewItem item = args.SelectedItem as NavigationViewItem;

                switch (item.Tag)
                {
                    case "home":
                        ContentFrame.Navigate(typeof(Login));
                        break;

                    case "members":
                        ContentFrame.Navigate(typeof(Membres.Membres));
                        break;

                    case "clubs":
                        ContentFrame.Navigate(typeof(Clubs.Clubs));
                        break;

                    case "spectators":
                        ContentFrame.Navigate(typeof(Login));
                        break;

                    case "tournaments":
                        ContentFrame.Navigate(typeof(Login));
                        break;
                }
            }
        }
    }
}
