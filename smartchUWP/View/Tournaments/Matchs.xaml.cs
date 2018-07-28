using Model;
using smartchUWP.View.Pages;
using smartchUWP.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

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
        private void MatchesPhases_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Pivot pivotView = sender as Pivot;
            MatchsPhase selectedMatchPhase = pivotView.SelectedItem as MatchsPhase;
            ((MatchsViewModel)DataContext).SelectedPhase = selectedMatchPhase;

        }
        private void OnAddPivotItem(object sender, EventArgs e)
        {

            MatchsViewModel pvm = (MatchsViewModel)this.DataContext;
            pvm.AddPivotItem();
            if (MatchPhasePivot.Items.Count == 1)
            {
                PivotItem pItem = (PivotItem)MatchPhasePivot.ContainerFromIndex(0);
                UIElement element = (UIElement)VisualTreeHelper.GetChild(pItem, 0);
                if (element != null && element.Visibility == Visibility.Collapsed)
                {
                    element.Visibility = Visibility.Visible;
                }

            }
        }
    }
}
