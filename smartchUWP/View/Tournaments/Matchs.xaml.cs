using Model;
using smartchUWP.View.Pages;
using smartchUWP.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace smartchUWP.View.Tournaments
{
    public sealed partial class Matchs : BindablePage
    {
        public Matchs()
        {
            this.InitializeComponent();
        }

        private void MatchsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ListView listView = sender as ListView;
            
            Match selectedMatch = listView.SelectedItem as Match;
            ((MatchsViewModel)DataContext).SelectedMatch = selectedMatch;

        }
    }
}
