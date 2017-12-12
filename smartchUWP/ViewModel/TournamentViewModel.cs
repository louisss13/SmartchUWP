using DataAccess;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Model;
using smartchUWP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartchUWP.ViewModel
{
    public class TournamentViewModel : MainPageViewModel
    {
        private readonly INavigationService _navigationService;
        public RelayCommand NavigateCommand { get; private set; }

        private ObservableCollection<Tournament> _tournaments = null;
        public TournamentViewModel(INavigationService navigationService):base(navigationService)
        {

            _navigationService = navigationService;
            NavigateCommand = new RelayCommand(NavigateToAddTournament);
            MessengerInstance.Register<NotificationMessage>(this, MessageReceiver);

            if (IsInDesignMode)
            {
                _tournaments = new ObservableCollection<Tournament> { new Tournament() { Name = "Tournois1" }, new Tournament() { Name = "Tounrnois2" } };
            }
            else
            {
                InitializeAsync();
            }
        }
        private void NavigateToAddTournament()
        {
            INavigationService navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            navigationService.NavigateTo("AddTournament");

        }
        public ObservableCollection<Tournament> Tournaments
        {
            get
            {
                return _tournaments;
            }
            set
            {
                if (_tournaments == value)
                {
                    return;
                }
                _tournaments = value;
                RaisePropertyChanged("Tournaments");
            }
        }
        public async Task InitializeAsync()
        {
           SetTournaments();   
        }
        private void MessageReceiver(NotificationMessage message) 
        {
            switch (message.VariableType)
            {
                case NotificationMessageType.ListTournament:
                    SetTournaments();
                    break;
            }
        }
        private async void SetTournaments()
        {
            var service = new TournamentsServices();
            var tournament = await service.GetTournaments();
            Tournaments = new ObservableCollection<Tournament>(tournament);
        }
    }
}
