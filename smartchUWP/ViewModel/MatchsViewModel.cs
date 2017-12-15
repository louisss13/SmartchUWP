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


        public RelayCommand CommandGenereMatch { get; private set; }

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
        public ObservableCollection<MatchsPhase> MatchsPhases
        {
            get
            {
                return new ObservableCollection<MatchsPhase>(SelectedTournament.Matches);
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
            CommandGenereMatch = new RelayCommand(GenereMatches, IsSelectedPhase);
            
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

        public void MessageReceiver(NotificationMessage message)
        {
            switch (message.VariableType)
            {
                case NotificationMessageType.Tournament:
                    SelectedTournament = (Tournament)message.Variable;
                    if (SelectedTournament.Matches == null)
                    {
                        SelectedTournament.Matches = new List<MatchsPhase>()
                    {
                        new MatchsPhase(){
                            NumPhase = 1,
                            Matchs = new List<Match>()
                        }
                    };

                    }
                    break;
            }
        }
    }
}
