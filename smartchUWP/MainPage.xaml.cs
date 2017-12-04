using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace smartchUWP
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
            NavView.MenuItems.Add(new NavigationViewItemSeparator());
            NavView.MenuItems.Add(new NavigationViewItem()
            { Content = "My content", Icon = new SymbolIcon(Symbol.Folder), Tag = "content" });

            // set the initial SelectedItem 
            foreach (NavigationViewItemBase item in NavView.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "apps")
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

                    case "Apps":
                        ContentFrame.Navigate(typeof(Login));
                        break;

                    case "Games":
                        ContentFrame.Navigate(typeof(Login));
                        break;

                    case "Music":
                        ContentFrame.Navigate(typeof(Login));
                        break;

                    case "My content":
                        ContentFrame.Navigate(typeof(Login));
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

                    case "apps":
                        ContentFrame.Navigate(typeof(Login));
                        break;

                    case "games":
                        ContentFrame.Navigate(typeof(Login));
                        break;

                    case "music":
                        ContentFrame.Navigate(typeof(Login));
                        break;

                    case "content":
                        ContentFrame.Navigate(typeof(Login));
                        break;
                }
            }
        }
    }
}
