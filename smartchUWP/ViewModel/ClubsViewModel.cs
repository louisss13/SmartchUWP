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

namespace smartchUWP.ViewModel
{
    public class ClubsViewModel : MainPageViewModel
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
           

            CmdNavigateAddClub = new RelayCommand(NavigateToAddClub);

            MessengerInstance.Register<NotificationMessage>(this, MessageReceiver);
            if (IsInDesignMode)
            {
                _clubs =   new ObservableCollection<Club>{ new Club() { Name = "Club1" }, new Club() { Name = "club2" } };
            }
            else
            {
               InitializeAsync();
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
        public async Task InitializeAsync()
        {
            SetClubs();
            
        }
        private void NavigateToAddClub()
        {
            _navigationService.NavigateTo("AddClub");
        }
        private async void SetClubs()
        {
            var service = new ClubsServices();
            var clubs = await service.GetClubs();
            ICollection<Club> allClubs = ((List<Object>) clubs.Content).Cast<Club>().ToList();
            Clubs = new ObservableCollection<Club>(allClubs);
        }
        private void MessageReceiver(NotificationMessage message)
        {
            switch (message.VariableType)
            {
                case NotificationMessageType.ListClub:
                    SetClubs();
                    
                    break;
            }
        }
        
    }
}
