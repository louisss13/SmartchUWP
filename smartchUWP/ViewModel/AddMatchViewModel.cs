using DataAccess;
using GalaSoft.MvvmLight;
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
    public class AddMatchViewModel : ViewModelBase
    {
        private Match _match;
        private User _selectedJoueur1;
        private User _selectedJoueur2;
        private Account _selectedArbitre;
        private TimeSpan _heurePrevue = new TimeSpan(12, 0, 0);
        private ObservableCollection<User> _allUsers = new ObservableCollection<User>();
        private ObservableCollection<User> _joueur1Liste = new ObservableCollection<User>();
        private ObservableCollection<User> _joueur2Liste = new ObservableCollection<User>();
        private ObservableCollection<User> _listeArbitre = new ObservableCollection<User>();
        private Tournament _tournament;

        public Match Match
        {
            get
            {
                return _match;
            }
            set
            {
                _match = value;
                if(value != null)
                {
                    SelectedJoueur1 = _match.Player1;
                    SelectedJoueur2 = _match.Player2;
                    SelectedArbitre = _match.Arbitre;

                }
                RaisePropertyChanged("Match");
            }
        }
        public User SelectedJoueur1
        {
            get
            {
                return _selectedJoueur1;
            }
            set
            {
                _selectedJoueur1 = value;
                int idJoueur = (SelectedJoueur2 != null)?SelectedJoueur2.Id:0;
                _joueur2Liste = new ObservableCollection<User>( AllUsers.ToList());
                _joueur2Liste.Remove(value);

                RaisePropertyChanged("SelectedJoueur1");
                
                

            }
        }
        public User SelectedJoueur2
        {
            get
            {
                return _selectedJoueur2;
            }
            set
            {
                _selectedJoueur2 = value;
                int idJoueur = (SelectedJoueur1 != null) ? SelectedJoueur1.Id : 0;
                _joueur1Liste = new ObservableCollection<User>(AllUsers.ToList());
                _joueur1Liste.Remove(value);
                
                if (idJoueur > 0)
                    _selectedJoueur1 = _joueur1Liste.Where(u => u.Id == idJoueur).First();
                RaisePropertyChanged("SelectedJoueur2");

                
                RaisePropertyChanged("SelectedJoueur1");

            }
        }
        public Account SelectedArbitre
        {
            get
            {
                return _selectedArbitre;
            }
            set
            {
                _selectedArbitre = value;
                RaisePropertyChanged("SelectedArbitre");
            }
        }
        public TimeSpan HeurePrevue
        {
            get
            {
                return _heurePrevue;
            }
            set
            {
                _heurePrevue = value;
                RaisePropertyChanged("HeurePrevue");
            }
        }
        public ObservableCollection<User> AllUsers
        {
            get
            {
                return _allUsers;
            }
            set
            {
                _allUsers = value;
                ListeJoueur1 = value;
                ListeJoueur2 = value;
                RaisePropertyChanged("AllUsers");
            }
        }
        public ObservableCollection<User> ListeJoueur1
        {
            get
            {
                return _joueur1Liste;
            }
            set
            {
                _joueur1Liste = value;
                RaisePropertyChanged("ListeJoueur1");
            }
        }
        public ObservableCollection<User> ListeJoueur2
        {
            get
            {
                return _joueur2Liste;
            }
            set
            {
                _joueur2Liste = value;
                RaisePropertyChanged("ListeJoueur2");
            }
        }
        public ObservableCollection<User> AllArbitre
        {
            get
            {
                return _listeArbitre;
            }
            set
            {
                _listeArbitre = value;
                RaisePropertyChanged("AllArbitre");
            }
        }
        public Tournament Tournament { get; set; }
        public int? NumPhase { get; set; }
        

        public AddMatchViewModel()
        {
            
            InitializeAsync();
            
        }
        

        private void MessageReceiver(NotificationMessage message)
        {
            switch (message.VariableType)
            {
                case NotificationMessageType.AddTournament:
                    if(message.Variable is List<Object>)
                    {
                        List<Object> variables = message.Variable as List<Object>;
                        Tournament = variables[0] as Tournament;
                        NumPhase = variables[1] as int?;
                        Match = variables[2] as Match;
                    }
                    else
                        Tournament = message.Variable as Tournament;
                break;
            }
        }

        public async void InitializeAsync()
        {
            UsersServices usersServices = new UsersServices();
            TournamentsServices tournamentsServices = new TournamentsServices();
            var users = await tournamentsServices.GetParticipants(1);
            var arbitres = await usersServices.GetUsersWithAccount();
            AllArbitre = new ObservableCollection<User>(arbitres);
            AllUsers = new ObservableCollection<User>(users);
            SelectedJoueur1 = AllUsers[0];
            MessengerInstance.Register<NotificationMessage>(this, MessageReceiver);
        }

    }
}
