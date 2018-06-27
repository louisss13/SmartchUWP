using DataAccess;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

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
    public class TournamentViewModel : SmartchViewModelBase
    {
        
        private ObservableCollection<Tournament> _tournaments = null;
        private Tournament _selectedTournament;

        public RelayCommand NavigateCommand { get; private set; }
        public RelayCommand CommandNavigateSelect { get; private set; }
        public RelayCommand CommandNavigateModification { get; private set; }

        public Tournament SelectedTournament
        {
            get
            {
                return _selectedTournament;
            }
            set
            {
                _selectedTournament = value;
                CommandNavigateSelect.RaiseCanExecuteChanged();
                RaisePropertyChanged("SelectedTournament");
            }
        }


        public TournamentViewModel(INavigationService navigationService) : base(navigationService)
        {

            
            NavigateCommand = new RelayCommand(NavigateToAddTournament);
            CommandNavigateSelect = new RelayCommand(NavigateToSelectTournament, IsSelected);
            CommandNavigateModification = new RelayCommand(NavigateToSelectTournament);

            MessengerInstance.Register<NotificationMessage>(this, MessageReceiver);

            if (IsInDesignMode)
            {
                _tournaments = new ObservableCollection<Tournament> { new Tournament() { Name = "Tournois1" }, new Tournament() { Name = "Tounrnois2" } };
            }
            else
            {
                SetTournaments();
            }
        }
        private bool IsSelected()
        {
            return SelectedTournament != null;
        }
        
        private void NavigateToSelectTournament()
        {
            
            _navigationService.NavigateTo("SelectTournament");
            MessengerInstance.Send(new NotificationMessage(NotificationMessageType.Tournament, SelectedTournament));

        }
        private void NavigateToAddTournament()
        {
            
            _navigationService.NavigateTo("AddTournament");

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
            try
            {
                List<Tournament> tournament = await service.GetTournaments();
                Tournaments = new ObservableCollection<Tournament>(tournament);
            }
            catch(Exception e)
            {
                SetGeneralErrorMessage(e);
            }

        }
    }
}
