using Model;
using smartchUWP.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace smartchUWP.View
{
    public sealed partial class MemberSelection : Page
    {
        public MemberSelection()
        {
            this.InitializeComponent();
            AllMembersListView.Items.All(i=> ((ListViewItem)i).IsSelected = true);
        }

        

        private void AllMembersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            ListView listView = sender as ListView;

            
            ObservableCollection<User> list = new ObservableCollection<User>();

           
            foreach (User item in listView.SelectedItems)
            { 
                list.Add(item);
            };

            ((IListeMembreViewModel)DataContext).SelectedAllMembers = list;
            
        }
        private void MembersEntityListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ListView listView = sender as ListView;
            ObservableCollection<User> list = new ObservableCollection<User>();

            foreach (User item in listView.SelectedItems)
            {
                list.Add(item);
            }

            ((IListeMembreViewModel)DataContext).SelectedMembersEntity = list;

        }
    }
}
