using DataAccess;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
    public class MatchsViewModel : ViewModelBase
    {
       
        private INavigationService _navigationService;
        private Tournament _selectedTournament = null;
        private MatchsPhase _selectedPhase;
        private Match _selectedMatch;


        public RelayCommand CommandNavigateAddMatch { get; private set; }
        public RelayCommand CommandGenereMatch { get; private set; }
        public RelayCommand CommandEditMatch { get; private set; }
        public RelayCommand CommandEnregistrerTournament { get; private set; }

        public Tournament SelectedTournament
        {
            get
            {
                return _selectedTournament;
            }
            set
            {
                _selectedTournament = value;
                RaisePropertyChanged("SelectedTournament");
                RaisePropertyChanged("MatchsPhases");
            }
        }
        public MatchsPhase SelectedPhase
        {
            get
            {
                return _selectedPhase;
            }
            set
            {
                _selectedPhase = value;
                CommandGenereMatch.RaiseCanExecuteChanged();
                RaisePropertyChanged("SelectedPhase");
            }
        }
        public Match SelectedMatch
        {
            get
            {
                return _selectedMatch;
            }
            set
            {
                _selectedMatch = value;
                CommandEditMatch.RaiseCanExecuteChanged();
                RaisePropertyChanged("SelectedMatch");
            }
        }
        public ObservableCollection<MatchsPhase> MatchsPhases
        {
            get
            {
                if(SelectedTournament != null && SelectedTournament.Matches != null) 
                    return new ObservableCollection<MatchsPhase>(SelectedTournament.Matches);
                return new ObservableCollection<MatchsPhase>(new List<MatchsPhase>());
            }
            set
            {
                SelectedTournament.Matches = value;
                RaisePropertyChanged("MatchsPhases");
            }
        }

        public MatchsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            MessengerInstance.Register<NotificationMessage>(this, MessageReceiver);

            CommandNavigateAddMatch = new RelayCommand(NavigateToAddMatch);
            CommandGenereMatch = new RelayCommand(GenereMatches, IsSelectedPhase);
            CommandEditMatch = new RelayCommand(NavigateToEditMatch, IsSelectedMatch);
            CommandEnregistrerTournament = new RelayCommand(RegisterTournamentAsync);
           
        }

        private void GenereMatches()
        {
            GenereFirstPhase();
        }
        private void GenereFirstPhase()
        {
            List<User> users = new List<User>(SelectedTournament.Participants);
            List<Match> matchs = new List<Match>();
            Random rand = new Random();
            while (users.Count > 1 ) {
                int toSkip = rand.Next(0, users.Count);
                User joueur1 = users.Skip(toSkip).Take(1).First();
                users.Remove(joueur1);
                toSkip = rand.Next(0, users.Count);
                User joueur2 = users.Skip(toSkip).Take(1).First();
                users.Remove(joueur2);
                Match match = new Match()
                {
                    Player1 = joueur1,
                    Player2 = joueur2,
                    State = EMatchState.EnPreparation
                };
                matchs.Add(match);
            }

            MatchsPhases.Where(m => m.NumPhase == 1).First().Matchs = matchs;
            MatchsPhases.Add(new MatchsPhase()
            {
                NumPhase = 2,
                Matchs = new List<Match>()
            });
            RaisePropertyChanged("MatchsPhases");
            
        }
        private bool IsSelectedPhase()
        {
            return SelectedPhase != null;
        }
        private bool IsSelectedMatch()
        {
            return SelectedMatch != null;
        }

        private void NavigateToEditMatch()
        {
            _navigationService.NavigateTo("AddMatch");
            NotificationMessage message = new NotificationMessage(NotificationMessageType.AddTournament, new List<Object>() { SelectedTournament, SelectedPhase.NumPhase, SelectedMatch });
            MessengerInstance.Send(message);
        }
        private void NavigateToAddMatch()
        {
            _navigationService.NavigateTo("AddMatch");
            NotificationMessage message = new NotificationMessage(NotificationMessageType.AddTournament, new List<Object>() { SelectedTournament, SelectedPhase.NumPhase });
            MessengerInstance.Send(message);
        }
        

        private async void RegisterTournamentAsync()
        {
            TournamentsServices tournamentsServices = new TournamentsServices();
            ResponseObject response = await tournamentsServices.UpdateAsync(SelectedTournament);
        }

        public async void MessageReceiver(NotificationMessage message)
        {
            switch (message.VariableType)
            {
                case NotificationMessageType.Tournament:
                    SelectedTournament = (Tournament)message.Variable;
                    TournamentsServices tournamentsServices = new TournamentsServices();
                    SelectedTournament = await tournamentsServices.GetTournament(SelectedTournament.Id);
                    if (SelectedTournament.Matches == null || SelectedTournament.Matches.Count() <= 0)
                    {
                        SelectedTournament.Matches = new List<MatchsPhase>()
                        {
                            new MatchsPhase(){
                                NumPhase = 1,
                                Matchs = new List<Match>()
                            }
                        };

                    }
                    SelectedPhase = MatchsPhases.Where(m => m.NumPhase == 1).First();


                    break;
            }
        }
    }
}
