using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DataAccess;

using smartchUWP.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Windows.UI.Notifications;
using smartchUWP.ViewModel.Interfaces;

namespace smartchUWP.ViewModel
{
    public class ClubsViewModel : SmartchViewModelBase, INavigable
    {
        public RelayCommand CmdNavigateAddClub { get; private set; }

        private ObservableCollection<Club> _clubs = null;
        private Club _selectedClub;
        
        
        public Club SelectedClub
        {
            get
            {
                return _selectedClub;
            }
            set
            {
                _selectedClub = value;
                RaisePropertyChanged("SelectedClub");
                RaisePropertyChanged("IsSelectedClub");
            }
        }
        public bool IsSelectedClub
        {
            get
            {
                return SelectedClub != null && SelectedClub.ClubId >0;
            }

        }


        public ClubsViewModel(INavigationService navigationService) : base(navigationService)
        {
            
            if (IsInDesignMode)
            {
                _clubs =   new ObservableCollection<Club>{ new Club() { Name = "Club1" }, new Club() { Name = "club2" } };
            }
            else
            {
                CmdNavigateAddClub = new RelayCommand(NavigateToAddClub);
                SetClubs();
            }
        }
        public ObservableCollection<Club> Clubs
        {
            get
            {
                return _clubs;
            }
            set
            {
                if (_clubs == value)
                {
                    return;
                }
                _clubs = value;
                RaisePropertyChanged("Clubs");
            }
        }

        private void NavigateToAddClub()
        {
            _navigationService.NavigateTo("AddClub");
        }
        private async void SetClubs()
        {
            var service = new ClubsServices();
            try
            {
                ICollection<Club> allClubs = await service.GetClubs();
                Clubs = new ObservableCollection<Club>(allClubs);
            }
            catch(Exception e)
            {
                SetGeneralErrorMessage(e);
            }
           
        }
     
        public void NavigatedTo(object parameter)
        {
            SetClubs();
        }

        public void NavigatedFrom(object parameter)
        {
            // nothig to do
        }
    }
}
