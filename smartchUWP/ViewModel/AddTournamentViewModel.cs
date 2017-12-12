using DataAccess;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Model;
using smartchUWP.Observable;
using smartchUWP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartchUWP.ViewModel
{
    public class AddTournamentViewModel : MainPageViewModel
    {
        private ObservableCollection<Club> _clubs = null;
        
        private ObservableTournament _tournament = new ObservableTournament( new Tournament() { Address = new Address() { Street = "rue de la loi" } });
        private ObservableCollection<KeyValuePair<TournamentState, string>> _tournamentStates = null;

        public RelayCommand CommandAddTournament { get; private set; }
        
        public AddTournamentViewModel(INavigationService navigationService):base(navigationService)
        {
            CommandAddTournament = new RelayCommand( AddTournament);
            if (IsInDesignMode)
            {
                _clubs = new ObservableCollection<Club> { new Club() { Name = "Club1" }, new Club() { Name = "club2" } };
            }
            else
            {
                InitializeAsync();
            }
        }
        public ObservableCollection<KeyValuePair<TournamentState, string>> TournamentStates
        {
            get
            {
                return _tournamentStates;
            }
            set
            {
                _tournamentStates = value;
                RaisePropertyChanged("TournamentStates");
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
        public ObservableTournament OTournament
        {
            get
            {
                return _tournament;
            }
            set
            {
                if (_tournament == value)
                {
                    return;
                }
                _tournament = value;
                RaisePropertyChanged("Tournament");
            }
        }


        public async void AddTournament()
        {
            Tournament tournament = OTournament;
            tournament.Admins = null;
            //tournament.Club = null;

            TournamentsServices tournamentsServices = new TournamentsServices();
            ResponseObject response = await tournamentsServices.AddTournamentAsync(tournament);
            if (response.Success)
            {
                MessengerInstance.Send(new NotificationMessage(NotificationMessageType.ListTournament));
            }
        }

        public async Task InitializeAsync()
        {
            SetClubList();
            TournamentStates = getTournamentState();

        }
        //TODO A mettre dans un converter plutot
        private ObservableCollection<KeyValuePair<TournamentState, string>> getTournamentState()
        {
            var stateDictionary = new ObservableCollection<KeyValuePair<TournamentState, string>>() ;
            foreach (TournamentState etat in Enum.GetValues(typeof(TournamentState)))
            {
                KeyValuePair<TournamentState, string> cleValeur;
                
                switch (etat)
                {
                    case TournamentState.EnCours:
                        cleValeur = new KeyValuePair<TournamentState, string>(etat, "En cours");
                        break;
                    case TournamentState.EnPreparation:
                        cleValeur = new KeyValuePair<TournamentState, string>(etat, "En Préparation"); 
                        break;
                    case TournamentState.Fini:
                        cleValeur = new KeyValuePair<TournamentState, string>(etat, "Fini");
                        break;
                    default:
                        cleValeur = new KeyValuePair<TournamentState, string>(etat, "Valeur non traduite");
                        break;

                }
                stateDictionary.Add(cleValeur);
                
            }
            
            return stateDictionary;
        }
        private void MessageReceiver(NotificationMessage message)
        {
            switch (message.VariableType)
            {
                case NotificationMessageType.ListTournament:
                    SetClubList();
                    break;
            }
        }
        private async void SetClubList()
        {
            var service = new ClubsServices();
            var clubs = await service.GetClubs();
            Clubs = new ObservableCollection<Club>(clubs);
        }
    }
}
   
